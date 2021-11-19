﻿using System;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Events;
using OzonEdu.StockApi.Domain.Exceptions;
using OzonEdu.StockApi.Domain.Exceptions.StockItemAggregate;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public class StockItem : Entity, IAggregationRoot
    {
        public StockItem(Sku sku,
            Name name,
            Item itemType,
            ClothingSize size,
            Quantity quantity,
            QuantityValue minimalQuantity)
        {
            Sku = sku;
            Name = name;
            ItemType = itemType;
            SetQuantity(quantity);
            SetMinimalQuantity(minimalQuantity);
            SetClothingSize(size);
        }

        public Sku Sku { get; }
        
        public Name Name { get; }
        
        public Item ItemType { get; }
        
        public ClothingSize ClothingSize { get; private set; }
        
        public Quantity Quantity { get; private set; }
        
        public QuantityValue MinimalQuantity { get; private set; }

        public void IncreaseQuantity(int valueToIncrease)
        {
            if (valueToIncrease < 0)
            {
                throw new NegativeValueException($"{nameof(valueToIncrease)} value is negative");
            }

            Quantity = new Quantity(Quantity.Value + valueToIncrease);
        }

        public void GiveOutItems(int valueToGiveOut)
        {
            if (valueToGiveOut < 0)
            {
                throw new NegativeValueException($"{nameof(valueToGiveOut)} value is negative");
            }

            if (Quantity.Value < valueToGiveOut)
            {
                throw new NotEnoughItemsException("Not enough items");
            }

            Quantity = new Quantity(Quantity.Value - valueToGiveOut);

            if (Quantity.Value <= MinimalQuantity.Value)
            {
                AddReachedMinimumDomainEvent(Sku);
            }
        }

        public void SetClothingSize(ClothingSize size)
        {
            if (size is not null &&
                (ItemType.Type.Equals(StockItemAggregate.ItemType.TShirtStarter) ||
                 ItemType.Type.Equals(StockItemAggregate.ItemType.TShirtAfterProbation) || 
                 ItemType.Type.Equals(StockItemAggregate.ItemType.TShirtСonferenceListener) || 
                 ItemType.Type.Equals(StockItemAggregate.ItemType.SweatshirtAfterProbation)) || 
                ItemType.Type.Equals(StockItemAggregate.ItemType.SweatshirtСonferenceSpeaker))
            {
                ClothingSize = size;
            }
            else if (size is null)
            {
                ClothingSize = null;
            }
            else
            {
                throw new StockItemSizeException($"Item with type {ItemType.Type.Name} cannot get size");    
            }
        }
        
        private void SetQuantity(Quantity value)
        {
            if (value.Value < 0)
            {
                throw new NegativeValueException($"{nameof(value)} value is negative");
            }

            Quantity = value;
        }

        private void SetMinimalQuantity(QuantityValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException($"{nameof(value)} is null");
            }

            if (value.Value is not null && value.Value < 0)
            {
                throw new NegativeValueException($"{nameof(value)} value is negative");
            }

            MinimalQuantity = value;
        }

        private void AddReachedMinimumDomainEvent(Sku sku)
        {
            var orderStartedDomainEvent = new ReachedMinimumStockItemsNumberDomainEvent(sku);
            AddDomainEvent(orderStartedDomainEvent);
        }
    }
}