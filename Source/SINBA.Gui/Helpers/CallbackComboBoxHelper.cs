using DevExpress.Web;
using DevExpress.Web.Mvc;
using Sinba.BusinessModel.Entity;
using Sinba.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinba.Gui.Helpers
{
    public static class CallbackComboBoxHelper
    {
        private static ComboBoxSettings callbackComboBox;

        public static MVCxColumnComboBoxProperties CreateComboBoxColumnProperties()
        {
            MVCxColumnComboBoxProperties p = new MVCxColumnComboBoxProperties();
            p.CallbackRouteValues = new { Controller = "Home", Action = "CountryComboBox" };
            p.ValueField = "CountryID";
            p.TextField = "CountryName";
            p.ValueType = typeof(int);
            p.CallbackPageSize = 20;
            p.DropDownStyle = DropDownStyle.DropDown;
            //p.BindList(ViewBag.Article);
            return p;
        }
        public static MVCxColumnComboBoxProperties CreateArticleComboBoxColumnProperties(object bindinglist)
        {
            MVCxColumnComboBoxProperties p = new MVCxColumnComboBoxProperties();
            p.CallbackRouteValues = new { Controller = SinbaConstants.Controllers.Contact, Action = SinbaConstants.Actions.ArticlePartial };
            p.ValueField = DbColumns.Id;
            p.TextField = DbColumns.Libelle;
            p.Columns.Add(DbColumns.Id);
            p.Columns.Add(DbColumns.Libelle);
            p.Columns.Add(DbColumns.PU);
            p.BindList(bindinglist);
            p.ValueType = typeof(string);
            p.CallbackPageSize = 50;
            p.DropDownStyle = DropDownStyle.DropDown;
            p.ClientSideEvents.BeginCallback = "OngridArticleBeginCallback";
            p.ClientSideEvents.LostFocus = "OngridArticleLostFocus";
            return p;
        }

        public static MVCxColumnComboBoxProperties GetArticleTransportComboBoxColumnProperties(object bindinglist)
        {
            MVCxColumnComboBoxProperties p = new MVCxColumnComboBoxProperties();
            p.CallbackRouteValues = new { Controller = SinbaConstants.Controllers.Contact, Action = SinbaConstants.Actions.ArticlePartial };
            p.ValueField = DbColumns.Id;
            p.TextField = DbColumns.Libelle;
            p.Columns.Add(DbColumns.Id);
            p.Columns.Add(DbColumns.Libelle);
            p.BindList(bindinglist);
            p.ValueType = typeof(string);
            p.CallbackPageSize = 50;
            p.DropDownStyle = DropDownStyle.DropDown;
            p.ClientSideEvents.BeginCallback = "OngridArticleBeginCallback";
            p.ClientSideEvents.LostFocus = "OngridArticleLostFocus";
            return p;
        }
        public static MVCxColumnComboBoxProperties GetArticleEmploiComboBoxColumnProperties(object bindinglist)
        {
            MVCxColumnComboBoxProperties p = new MVCxColumnComboBoxProperties();
            p.CallbackRouteValues = new { Controller = SinbaConstants.Controllers.Contact, Action = SinbaConstants.Actions.ArticlePartial };
            p.ValueField = DbColumns.Id;
            p.TextField = DbColumns.Libelle;
            p.Columns.Add(DbColumns.Id);
            p.Columns.Add(DbColumns.Libelle);
            p.BindList(bindinglist);
            p.ValueType = typeof(string);
            p.CallbackPageSize = 50;
            p.DropDownStyle = DropDownStyle.DropDown;
            p.ClientSideEvents.BeginCallback = "OngridArticleBeginCallback";
            p.ClientSideEvents.LostFocus = "OngridArticleLostFocus";
            return p;
        }
        public static MVCxColumnComboBoxProperties GetCentreDeCoutSiteComboBoxColumnProperties(object bindinglist)
        {
            MVCxColumnComboBoxProperties p = new MVCxColumnComboBoxProperties();
            p.CallbackRouteValues = new { Controller = SinbaConstants.Controllers.Contact, Action = SinbaConstants.Actions.VentilationsPartial };
            p.ValueField = DbColumns.Id;
            p.TextField = DbColumns.Libelle;
            p.Columns.Add(DbColumns.Libelle);
            p.BindList(bindinglist);
            p.ValueType = typeof(string);
            p.CallbackPageSize = 50;
            p.DropDownStyle = DropDownStyle.DropDown;
            p.ClientSideEvents.BeginCallback = "OnCentreDeCoutBeginCallback";
            p.ClientSideEvents.LostFocus = "OnCentreDeCoutSelectedIndexChanged";
            return p;
        }


    }
}