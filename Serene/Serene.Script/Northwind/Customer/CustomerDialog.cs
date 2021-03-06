﻿
namespace Serene.Northwind
{
    using jQueryApi;
    using Serenity;
    using System.Collections.Generic;

    [IdProperty("ID"), NameProperty("CustomerID"), Flexify, Maximizable]
    [FormKey("Northwind.Customer"), LocalTextPrefix("Northwind.Customer"), Service("Northwind/Customer")]
    public class CustomerDialog : EntityDialog<CustomerRow>, IAsyncInit
    {
        private CustomerOrdersGrid ordersGrid;

        public CustomerDialog()
        {
            ordersGrid = new CustomerOrdersGrid(this.ById("OrdersGrid"));
            ordersGrid.Element.FlexHeightOnly();

            tabs.OnActivate += (e, i) => this.Arrange();
        }

        protected override void LoadEntity(CustomerRow entity)
        {
            base.LoadEntity(entity);

            tabs.SetDisabled("Orders", this.IsNewOrDeleted);

            ordersGrid.CustomerID = entity.CustomerID;
        }

        protected override void OnSaveSuccess(ServiceResponse response)
        {
            base.OnSaveSuccess(response);

            Q.ReloadLookup(CustomerRow.LookupKey);
        }
    }
}