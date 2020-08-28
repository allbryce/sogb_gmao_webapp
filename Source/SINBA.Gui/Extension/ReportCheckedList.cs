using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sinba.Gui.Extension
{
    public class CheckedListWindowTemplate: ITemplate {
        private CheckedListWindowSettings _settings;        

        public CheckedListWindowTemplate(CheckedListWindowSettings settings) {
            this._settings = settings;
        }

        protected CheckedListWindowSettings Settings { get { return _settings; } }

        public void InstantiateIn(Control container) {            
            ASPxListBox listBox = new ASPxListBox() { ID = Settings.ListBoxName };
            container.Controls.Add(listBox);

            listBox.ClientInstanceName = Settings.ListBoxName;            
            listBox.Border.BorderWidth = 0;
            listBox.BorderBottom.BorderWidth = 1;
            listBox.BorderBottom.BorderColor = Color.FromArgb(0xDCDCDC);
            listBox.Width = Unit.Percentage(100);                     
            
            listBox.SelectionMode = ListEditSelectionMode.CheckColumn;
            listBox.ClientSideEvents.SelectedIndexChanged = String.Format("function(s, e){{ OnListBoxSelectionChanged(s, e, {0}); }}", Settings.CheckComboBoxName);

            if (Settings.DataSource == null) {
                listBox.Items.AddRange(Settings.Items);
                listBox.Items.Insert(0, new ListEditItem("(Select all)"));
            }
            else {
                listBox.TextField = Settings.TextField;
                listBox.ValueField = Settings.TextField;
                listBox.ValueType = typeof(string);
                listBox.DataSource = Settings.DataSource;
                listBox.DataBound += new EventHandler(listBox_DataBound);
                listBox.DataBindItems();
            }

            container.Controls.Add(new LiteralControl("<div style=\"padding: 6px; height: 24px;\">"));

            ASPxButton button = new ASPxButton() { ID = Settings.CloseButtonName };
            container.Controls.Add(button);

            button.ClientInstanceName = Settings.CloseButtonName;
            button.Text = "Close";
            button.Style.Add("float", "right");
            button.Style.Add("padding", "0px 2px");
            button.ClientSideEvents.Click = String.Format("function(s, e){{ {0}.HideDropDown(); }}", Settings.CheckComboBoxName);
            button.Height = 26;

            container.Controls.Add(new LiteralControl("</div>"));
        }

        void listBox_DataBound(object sender, EventArgs e) {
            ASPxListBox listBox = sender as ASPxListBox;
            listBox.Items.Insert(0, new ListEditItem("(Select all)"));
        }
    }

    public class CheckedListWindowSettings {
        private string _checkComboBoxName;
        private ListEditItemCollection _items;

        public CheckedListWindowSettings(string checkComboBoxName) {
            this._items = new ListEditItemCollection();
            this._checkComboBoxName = checkComboBoxName;
            this.TextField = String.Empty;
            this.DataSource = null;
        }

        public string CheckComboBoxName { get { return _checkComboBoxName; } }
        public string ListBoxName { get { return String.Format("{0}_ListBox", this.CheckComboBoxName); } }
        public string CloseButtonName { get { return String.Format("{0}_ButtonClose", this.CheckComboBoxName); } }
        public ListEditItemCollection Items { get { return _items; } }

        public string TextField { get; set; }
        public object DataSource { get; set;  }        
    }
}