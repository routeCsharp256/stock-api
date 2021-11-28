using System;
using System.Collections.Generic;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Events;
using OzonEdu.StockApi.Domain.Exceptions.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate
{
    /// <summary>
    /// Заявка на пополнение склада товарных позиций
    /// </summary>
    public class DeliveryRequest : Entity
    {
        public DeliveryRequest(
            RequestNumber requestNumber,
            RequestStatus requestStatus,
            IReadOnlyList<Sku> skuCollection)
        {
            RequestNumber = requestNumber;
            RequestStatus = requestStatus;
            SkuCollection = skuCollection;
        }

        /// <summary>
        /// Номер заявки
        /// </summary>
        public RequestNumber RequestNumber { get; private set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        public RequestStatus RequestStatus { get; private set; }

        /// <summary>
        /// Идентификаторы позиций в заявке
        /// </summary>
        public IReadOnlyList<Sku> SkuCollection { get; }

        /// <summary>
        /// Установить номер заказа
        /// </summary>
        /// <param name="number">Номер заказа</param>
        public void SetRequestNumber(RequestNumber number)
        {
            RequestNumber = number;
        }
        
        /// <summary>
        /// Установить номер заказа
        /// </summary>
        /// <param name="number">Номер заказа</param>
        public void SetRequestNumber(long number)
        {
            RequestNumber = new RequestNumber(number);
        }

        /// <summary>
        /// Смена статуса у заявки на пополнение склада
        /// </summary>
        /// <param name="status"></param>
        /// <exception cref="Exception"></exception>
        public void ChangeStatus(RequestStatus status)
        {
            if (RequestStatus.Equals(RequestStatus.Done))
            {
                throw new DeliveryRequestStatusException($"Request in done. Change status unavailable");
            }

            RequestStatus = status;
        }
    }
}