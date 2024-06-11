using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Wallet.Currencies;
using Wallet.SystemGroups;

namespace Wallet.WalletController
{
    [UpdateInGroup(typeof(WalletSimulationSystemsGroup))]
    [BurstCompile]
    public partial struct SetCurrentControlledCurrencyIdSystem : ISystem
    {

        [BurstCompile]        
        public void OnUpdate(ref SystemState systemState)
        {
            if (SystemAPI.TryGetSingleton(out ChangeCurrencyIdRequestComponent request))
            {
                var isIncrease = request.IsIncrease;
                using var ecb = new EntityCommandBuffer(Allocator.Temp);
                var controlState = SystemAPI.GetSingletonRW<CurrencyControlStateComponent>();
                var currentId = controlState.ValueRO.CurrentCurrencyId;
                var maxId = controlState.ValueRO.MaxId;
                var minId = controlState.ValueRO.MinId;
                var nextId = isIncrease ? currentId + 1 : currentId - 1;
                if (nextId < minId)
                {
                    nextId = maxId;
                }
                else if (nextId > maxId)
                {
                    nextId = minId;
                }
                else
                {
                    var nearestId = isIncrease ? maxId : minId;
                    foreach (var currencyId in SystemAPI.Query<RefRO<CurrencyIdComponent>>())
                    {
                        var id = currencyId.ValueRO.Id;
                        if (id == nextId)
                        {
                            nearestId = nextId;
                            break;
                        }

                        if (isIncrease)
                        {
                            if (id > nextId && id < nearestId)
                            {
                                nearestId = id;
                            }
                        }
                        else
                        {
                            if (id < nextId && id > nearestId)
                            {
                                nearestId = id;
                            }
                        }
                    }

                    nextId = nearestId;
                }
                controlState.ValueRW.CurrentCurrencyId = nextId;
            }
        }
    }
}