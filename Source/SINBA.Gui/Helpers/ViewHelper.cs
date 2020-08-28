using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using Sinba.BusinessModel.Enums;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Sinba.Gui.Helpers
{
    public enum JSEvents
    {
        Init,
        ValueChanged,
        SelectedIndexChanged,
        LostFocus,
        GotFocus,
        BeginCallback,
        DropDown,
        EndCallback,
        CheckedChanged,
        KeyDown,
        KeyPress,
        KeyUp
    }
    /// <summary>
    /// Helper to render graphical controls in Razor Views
    /// </summary>
    public class ViewHelper
    {
        #region Methods
        #region Form / FormLayout
        #region TextBox

        /// <summary>
        /// FormLayout TextBox settings.
        /// </summary>
        /// <param name="textBox">The item.</param>
        /// <param name="name">Item name.</param>
        /// <param name="colSpan"></param>
        /// <param name="caption">Item caption.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="focus">if set to <c>true</c> [focus].</param>
        /// <param name="isPassword">if set to <c>true</c> [password].</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="mask"></param>
        /// <param name="maskIncludeLiteralMode">The mask include literal mode.</param>
        /// <param name="maskPromptChar">The mask prompt character.</param>
        /// <param name="maskErrorText">The mask error text.</param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutTextBoxSettings(MVCxFormLayoutItem textBox, int? colSpan = null,
            string caption = null, int? maxLength = null, bool enabled = true,
            bool focus = false, bool isPassword = false, bool showModelErrors = true,
            string mask = null, MaskIncludeLiteralsMode maskIncludeLiteralMode = MaskIncludeLiteralsMode.None, int? widthPixel = null,
            char maskPromptChar = Strings.MaskPromptChar, string maskErrorText = null, string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup,
            params JSEvents[] clientSideEvents)
        {
            if (!string.IsNullOrEmpty(caption)) { textBox.Caption = caption; }
            if (colSpan.HasValue) { textBox.ColSpan = colSpan.Value; }
            textBox.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox;
            TextBoxSettings textBoxSettings = (TextBoxSettings)textBox.NestedExtensionSettings;
            TextBoxSettings(textBoxSettings,
                maxLength: maxLength,
                enabled: enabled,
                widthPixel: widthPixel,
                focus: focus,
                isPassword: isPassword,
                showModelErrors: showModelErrors,
                mask: mask,
                maskIncludeLiteralMode: maskIncludeLiteralMode,
                maskPromptChar: maskPromptChar,
                maskErrorText: maskErrorText,
                helpText: helpText,
                helpTextDisplayMode: helpTextDisplayMode,
                clientSideEvents: clientSideEvents);
        }

        /// <summary>
        /// TextBox settings.
        /// </summary>
        /// <param name="textBoxSettings">The text box settings.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="focus">if set to <c>true</c> [focus].</param>
        /// <param name="isPassword">if set to <c>true</c> [is password].</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="mask">The mask.</param>
        /// <param name="maskIncludeLiteralMode">The mask include literal mode.</param>
        /// <param name="maskPromptChar">The mask prompt character.</param>
        /// <param name="maskErrorText">The mask error text.</param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void TextBoxSettings(TextBoxSettings textBoxSettings,
            int? maxLength = null, bool enabled = true,
            bool focus = false, bool isPassword = false, bool showModelErrors = true, int? widthPixel = null,
            string mask = null, MaskIncludeLiteralsMode maskIncludeLiteralMode = MaskIncludeLiteralsMode.None,
            char maskPromptChar = Strings.MaskPromptChar, string maskErrorText = null, string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup,
            params JSEvents[] clientSideEvents)
        {
            textBoxSettings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
            textBoxSettings.ControlStyle.CssClass = CssClasses.Editor;
            textBoxSettings.ShowModelErrors = showModelErrors;
            textBoxSettings.Properties.Password = isPassword;
            textBoxSettings.Enabled = enabled;
            if (widthPixel.HasValue)
            {
                textBoxSettings.Width = Unit.Pixel(widthPixel.Value);
            }
            if (maxLength.HasValue)
            {
                textBoxSettings.Properties.MaxLength = maxLength.Value;
            }
            if (focus)
            {
                textBoxSettings.Properties.ClientSideEvents.Init = Strings.FunctionFocus;
            }

            // HelpText
            if (!string.IsNullOrWhiteSpace(helpText))
            {
                textBoxSettings.Properties.HelpText = helpText;
                textBoxSettings.Properties.HelpTextSettings.DisplayMode = helpTextDisplayMode;
                if (helpTextDisplayMode == HelpTextDisplayMode.Popup)
                {
                    textBoxSettings.Properties.HelpTextSettings.EnablePopupAnimation = true;
                }
                else if (helpTextDisplayMode == HelpTextDisplayMode.Inline)
                {
                    textBoxSettings.Properties.HelpTextSettings.Position = HelpTextPosition.Bottom;
                }
            }

            // Mask
            if (!string.IsNullOrWhiteSpace(mask))
            {
                textBoxSettings.Properties.MaskSettings.Mask = mask;
                textBoxSettings.Properties.MaskSettings.IncludeLiterals = maskIncludeLiteralMode;
                textBoxSettings.Properties.MaskSettings.PromptChar = ' ';
                textBoxSettings.Properties.MaskSettings.ErrorText = string.IsNullOrWhiteSpace(maskErrorText) ? EntityCommonResource.errorWrongFormat : maskErrorText;
                textBoxSettings.Properties.ValidationSettings.Display = Display.Dynamic;
            }
            if (!string.IsNullOrWhiteSpace(textBoxSettings.Name) && clientSideEvents.Length > 0)
            {
                clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => textBoxSettings.Properties.ClientSideEvents.SetEventHandler(e, textBoxSettings.Name.Add(e)));
            }

        }
        #endregion

        #region Numeric textBox

        /// <summary>
        /// Forms the layout numeric text box settings.
        /// </summary>
        /// <param name="numericTextBox">The numeric text box.</param>
        /// <param name="colSpan">The col span.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="focus">if set to <c>true</c> [focus].</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="displayFormatString"></param>
        /// <param name="numberType">Type of the number.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="number"></param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutNumericTextBoxSettings(MVCxFormLayoutItem numericTextBox, int? colSpan = null,
            string caption = null, int? maxLength = null, bool enabled = true, bool required = false, bool clientVisible = true,
            bool focus = false, bool showModelErrors = true, string displayFormatString = null, bool displayFormatInEditMode = true, int? widthPixel = null,
            SpinEditNumberType numberType = SpinEditNumberType.Integer, decimal maxValue = 0, decimal minValue = 0, decimal? number = null,
            string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup, params JSEvents[] clientSideEvents)
        {
            FormLayoutSpinEditSettings(numericTextBox,
                colSpan: colSpan,
                caption: caption,
                maxLength: maxLength,
                enabled: enabled,
                required: required,
                focus: focus,
                showModelErrors: showModelErrors,
                numberType: numberType,
                showIncrementButtons: false,
                clientVisible: clientVisible,
                maxValue: maxValue,
                minValue: minValue,
                widthPixel: widthPixel,
                displayFormatString: displayFormatString,
                displayFormatInEditMode: displayFormatInEditMode,
                number: number,
                helpText: helpText,
                helpTextDisplayMode: helpTextDisplayMode,
                clientSideEvents: clientSideEvents);
        }
        #endregion

        #region SpinEdit

        /// <summary>
        /// Forms the layout spin edit settings.
        /// </summary>
        /// <param name="spinEdit">The spin edit.</param>
        /// <param name="colSpan">The col span.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="focus">if set to <c>true</c> [focus].</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="displayFormatString"></param>
        /// <param name="showIncrementButtons">if set to <c>true</c> [show increment buttons].</param>
        /// <param name="showLargeIncrementButtons">if set to <c>true</c> [show large increment buttons].</param>
        /// <param name="increment">The increment.</param>
        /// <param name="numberFormat">The number format.</param>
        /// <param name="numberType">Type of the number.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="number"></param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutSpinEditSettings(MVCxFormLayoutItem spinEdit, int? colSpan = null,
            string caption = null, int? maxLength = null, bool enabled = true, bool required = false,
            bool focus = false, bool showModelErrors = true, string displayFormatString = null, bool displayFormatInEditMode = true,
            bool showIncrementButtons = true, bool showLargeIncrementButtons = false, decimal increment = 0,
            SpinEditNumberFormat numberFormat = SpinEditNumberFormat.Number,
            int? widthPixel = null, bool clientVisible = true,
            SpinEditNumberType numberType = SpinEditNumberType.Integer, decimal maxValue = 0, decimal minValue = 0, decimal? number = null,
            string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup, params JSEvents[] clientSideEvents)
        {
            if (!string.IsNullOrWhiteSpace(caption)) { spinEdit.Caption = caption; }
            if (colSpan.HasValue) { spinEdit.ColSpan = colSpan.Value; }
            spinEdit.NestedExtensionType = FormLayoutNestedExtensionItemType.SpinEdit;
            SpinEditSettings spinEditSettings = (SpinEditSettings)spinEdit.NestedExtensionSettings;
            SpinEditSettings(spinEditSettings,
                maxLength: maxLength,
                enabled: enabled,
                required: required,
                focus: focus,
                showModelErrors: showModelErrors,
                showIncrementButtons: showIncrementButtons,
                showLargeIncrementButtons: showLargeIncrementButtons,
                increment: increment,
                numberFormat: numberFormat,
                numberType: numberType,
                maxValue: maxValue,
                minValue: minValue,
                widthPixel: widthPixel,
                displayFormatString: displayFormatString,
                displayFormatInEditMode: displayFormatInEditMode,
                number: number,
                clientVisible: clientVisible,
                helpText: helpText,
                helpTextDisplayMode: helpTextDisplayMode,
                clientSideEvents: clientSideEvents);
        }

        /// <summary>
        /// Spins the edit settings.
        /// </summary>
        /// <param name="spinEditSettings">The spin edit settings.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="focus">if set to <c>true</c> [focus].</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="displayFormatString"></param>
        /// <param name="showIncrementButtons">if set to <c>true</c> [show increment buttons].</param>
        /// <param name="showLargeIncrementButtons">if set to <c>true</c> [show large increment buttons].</param>
        /// <param name="increment">The increment.</param>
        /// <param name="numberFormat">The number format.</param>
        /// <param name="numberType">Type of the number.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="number"></param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void SpinEditSettings(SpinEditSettings spinEditSettings,
            int? maxLength = null, bool enabled = true, bool required = false,
            bool focus = false, bool showModelErrors = true, string displayFormatString = null, bool displayFormatInEditMode = true,
            bool showIncrementButtons = true, bool showLargeIncrementButtons = false, decimal increment = 0,
            SpinEditNumberFormat numberFormat = SpinEditNumberFormat.Number,
            int? widthPixel = null, bool clientVisible = true,
            SpinEditNumberType numberType = SpinEditNumberType.Integer, decimal maxValue = 0, decimal minValue = 0, decimal? number = null,
            string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup, params JSEvents[] clientSideEvents)
        {
            spinEditSettings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
            spinEditSettings.ControlStyle.CssClass = CssClasses.Editor;
            spinEditSettings.ShowModelErrors = showModelErrors;
            spinEditSettings.Enabled = enabled;
            spinEditSettings.ClientVisible = clientVisible;
            if (required)
            {
                spinEditSettings.Properties.ValidationSettings.RequiredField.IsRequired = true;
                spinEditSettings.Properties.ValidationSettings.RequiredField.ErrorText = string.Format(EntityCommonResource.errorRequiredField, spinEditSettings.Name);
            }
            if (widthPixel.HasValue)
            {
                spinEditSettings.Width = Unit.Pixel(widthPixel.Value);
            }
            if (maxLength.HasValue)
            {
                spinEditSettings.Properties.MaxLength = maxLength.Value;
            }
            if (focus)
            {
                spinEditSettings.Properties.ClientSideEvents.Init = Strings.FunctionFocus;
            }

            if (!string.IsNullOrWhiteSpace(helpText))
            {
                spinEditSettings.Properties.HelpText = helpText;
                spinEditSettings.Properties.HelpTextSettings.DisplayMode = helpTextDisplayMode;
                if (helpTextDisplayMode == HelpTextDisplayMode.Popup)
                {
                    spinEditSettings.Properties.HelpTextSettings.EnablePopupAnimation = true;
                }
                else if (helpTextDisplayMode == HelpTextDisplayMode.Inline)
                {
                    spinEditSettings.Properties.HelpTextSettings.Position = HelpTextPosition.Bottom;
                }

            }
            spinEditSettings.Properties.SpinButtons.ShowIncrementButtons = showIncrementButtons;
            spinEditSettings.Properties.SpinButtons.ShowLargeIncrementButtons = showLargeIncrementButtons;
            spinEditSettings.Properties.Increment = increment;
            spinEditSettings.Properties.NumberType = numberType;
            spinEditSettings.Properties.NumberFormat = numberFormat;
            spinEditSettings.Properties.MinValue = minValue;
            spinEditSettings.Properties.MaxValue = maxValue;
            if (numberType == SpinEditNumberType.Float)
            {
                spinEditSettings.Properties.DisplayFormatString = string.IsNullOrWhiteSpace(displayFormatString) ? "{0:0.##}" : displayFormatString;
                spinEditSettings.Properties.DisplayFormatInEditMode = displayFormatInEditMode;
            }
            if (number.HasValue) { spinEditSettings.Number = number.Value; }
            if (!string.IsNullOrWhiteSpace(spinEditSettings.Name) && clientSideEvents.Length > 0)
            {
                clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => spinEditSettings.Properties.ClientSideEvents.SetEventHandler(e, spinEditSettings.Name.Add(e)));
            }
        }
        #endregion

        #region CheckBox

        /// <summary>
        /// Forms the layout CheckBox setting.
        /// </summary>
        /// <param name="checkBox">The item.</param>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="textAlign"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutCheckBoxSetting(MVCxFormLayoutItem checkBox,
            string name = null, string displayName = null, TextAlign textAlign = TextAlign.Left, params JSEvents[] clientSideEvents)
        {
            if (textAlign == TextAlign.Right) { checkBox.Caption = string.Empty; }
            checkBox.VerticalAlign = FormLayoutVerticalAlign.Middle;
            checkBox.RequiredMarkDisplayMode = FieldRequiredMarkMode.Hidden;
            checkBox.ParentContainerStyle.Paddings.PaddingTop = Unit.Pixel(0);
            checkBox.NestedExtensionType = FormLayoutNestedExtensionItemType.CheckBox;
            checkBox.Name = name;
            CheckBoxSettings checkBoxSettings = (CheckBoxSettings)checkBox.NestedExtensionSettings;
            checkBoxSettings.Properties.ValueChecked = true;
            checkBoxSettings.Properties.ValueUnchecked = false;
            if (textAlign == TextAlign.Right)
            {
                checkBoxSettings.Text = displayName;
                checkBoxSettings.PreRender = (sender, e) => { ((MVCxCheckBox)sender).TextAlign = TextAlign.Right; };
            }
            if (!string.IsNullOrWhiteSpace(checkBoxSettings.Name))
            {
                if (string.IsNullOrWhiteSpace(checkBox.Name)) { checkBox.Name = checkBoxSettings.Name; }
                if (clientSideEvents.Length > 0)
                {
                    clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => checkBoxSettings.Properties.ClientSideEvents.SetEventHandler(e, checkBoxSettings.Name.Add(e)));
                }
            }
        }
        #endregion

        #region Label

        /// <summary>
        /// Forms the layout label settings.
        /// </summary>
        /// <param name="label">The item.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="associatedControlName"></param>
        /// <param name="isRequired"></param>
        public static void FormLayoutLabelSettings(MVCxFormLayoutItem label, string caption = null, string labelText = null, string associatedControlName = null, bool isRequired = false)
        {
            if (!string.IsNullOrWhiteSpace(caption)) { label.Caption = caption; }
            label.NestedExtensionType = FormLayoutNestedExtensionItemType.Label;
            label.CaptionSettings.AssociatedNestedExtensionName = label.Name;
            LabelSettings labelSettings = (LabelSettings)label.NestedExtensionSettings;
            LabelSettings(labelSettings, labelText, associatedControlName, isRequired);
        }

        /// <summary>
        /// Labels the settings.
        /// </summary>
        /// <param name="labelSettings">The label settings.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="associatedControlName">Name of the associated control.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public static void LabelSettings(LabelSettings labelSettings, string labelText = null, string associatedControlName = null, bool isRequired = false)
        {
            if (!string.IsNullOrWhiteSpace(associatedControlName)) { labelSettings.AssociatedControlName = associatedControlName; }
            labelSettings.ControlStyle.CssClass = string.Format("{0} {1}", CssClasses.FormLayoutLabel, isRequired ? CssClasses.LabelRequired : CssClasses.Label);
            labelSettings.Text = labelText;
        }
        #endregion

        #region HyperLink
        /// <summary>
        /// Forms the layout hyper link settings.
        /// </summary>
        /// <param name="hyperLink">The hyper link.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="fontBold">if set to <c>true</c> [font bold].</param>
        /// <param name="text">The text.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        public static void FormLayoutHyperLinkSettings(MVCxFormLayoutItem hyperLink,
            string caption = null, bool fontBold = false, string text = null,
            string controller = null, string action = null)
        {
            hyperLink.Caption = string.IsNullOrWhiteSpace(caption) ? string.Empty : caption;
            hyperLink.CaptionStyle.Font.Bold = fontBold;
            hyperLink.NestedExtensionType = FormLayoutNestedExtensionItemType.HyperLink;
            HyperLinkSettings hyperLinkSettings = (HyperLinkSettings)hyperLink.NestedExtensionSettings;
            hyperLinkSettings.Properties.Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
            if (!string.IsNullOrWhiteSpace(action))
            {
                if (!string.IsNullOrWhiteSpace(controller))
                {
                    hyperLinkSettings.NavigateUrl = DevExpressHelper.GetUrl(new { Controller = controller, Action = action });
                }
                else
                {
                    hyperLinkSettings.NavigateUrl = DevExpressHelper.GetUrl(new { Action = action });
                }
            }
        }
        #endregion

        #region ComboBox

        /// <summary>
        /// Forms the layout ComboBox settings.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="callbackRouteValues"></param>
        /// <param name="caption">The caption.</param>
        /// <param name="textField">The text field.</param>
        /// <param name="valueField">The value field.</param>
        /// <param name="clearButtonDisplayMode"></param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="colSpan"></param>
        /// <param name="dataSource"></param>
        /// <param name="enabled"></param>
        /// <param name="displayColumns"></param>
        /// <param name="helpText"></param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutComboBoxSettings(MVCxFormLayoutItem comboBox, string name = null, Type valueType = null, int? colSpan = null, object callbackRouteValues = null,
            string caption = null, string textField = DbColumns.Libelle, string valueField = DbColumns.Id, string textFormatString = "{0}", int? widthPixel = null, int? dropDownWidth = null,
            ClearButtonDisplayMode clearButtonDisplayMode = ClearButtonDisplayMode.OnHover, bool showModelErrors = true, object dataSource = null, bool enabled = true, bool isreadonly = false, bool openOnFocus = true,
            string[] displayColumns = null, string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup, params JSEvents[] clientSideEvents)
        {
            if (!string.IsNullOrWhiteSpace(caption)) { comboBox.Caption = caption; }
            if (colSpan.HasValue) { comboBox.ColSpan = colSpan.Value; }
            comboBox.NestedExtensionType = FormLayoutNestedExtensionItemType.ComboBox;
            ComboBoxSettings cbxSettings = (ComboBoxSettings)comboBox.NestedExtensionSettings;
            if (!string.IsNullOrWhiteSpace(name))
            {
                cbxSettings.Name = name;
            }
            ComboBoxSettings(cbxSettings,
                callbackRouteValues: callbackRouteValues,
                valueType: valueType,
                textField: textField,
                valueField: valueField,
                clearButtonDisplayMode: clearButtonDisplayMode,
                showModelErrors: showModelErrors,
                dataSource: dataSource,
                enabled: enabled,
                displayColumns: displayColumns,
                textFormatString: textFormatString,
                helpText: helpText,
                openOnFocus: openOnFocus,
                helpTextDisplayMode: helpTextDisplayMode,
                widthPixel: widthPixel,
                dropdownWidth: dropDownWidth,
                clientSideEvents: clientSideEvents);
        }

        /// <summary>
        /// ComboBoxes the settings.
        /// </summary>
        /// <param name="comboBoxSettings">The combo box settings.</param>
        /// <param name="name"></param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="callbackPageSize"></param>
        /// <param name="textField">The text field.</param>
        /// <param name="valueField">The value field.</param>
        /// <param name="clearButtonDisplayMode">The clear button display mode.</param>
        /// <param name="showModelErrors">if set to <c>true</c> [show model errors].</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="displayColumns">The display columns.</param>
        /// <param name="helpTextDisplayMode"></param>
        /// <param name="clientSideEvents">The client side events.</param>
        /// <param name="widthPercentage"></param>
        /// <param name="widthPixel"></param>
        /// <param name="callbackRouteValues"></param>
        /// <param name="helpText"></param>
        public static void ComboBoxSettings(ComboBoxSettings comboBoxSettings, string name = null, Type valueType = null, int? widthPercentage = null, bool openOnFocus = true,
            int? widthPixel = null, int? dropdownWidth = null, object callbackRouteValues = null, int callbackPageSize = 10, string textField = DbColumns.Libelle, string valueField = DbColumns.Id,
            ClearButtonDisplayMode clearButtonDisplayMode = ClearButtonDisplayMode.OnHover, bool showModelErrors = true, object dataSource = null, bool enabled = true, bool isreadonly = false, string textFormatString = "{0}", string caption = null,
            string[] displayColumns = null, string helpText = null, HelpTextDisplayMode helpTextDisplayMode = HelpTextDisplayMode.Popup, params JSEvents[] clientSideEvents)
        {
            if (widthPercentage.HasValue)
            {
                comboBoxSettings.Width = Unit.Percentage(widthPercentage.Value);
            }
            if (widthPixel.HasValue)
            {
                comboBoxSettings.Width = Unit.Pixel(widthPixel.Value);
            }
            if (callbackRouteValues != null)
            {
                comboBoxSettings.CallbackRouteValues = callbackRouteValues;
                comboBoxSettings.Properties.CallbackPageSize = callbackPageSize;
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                comboBoxSettings.Name = name;
            }
            if (openOnFocus) { comboBoxSettings.Properties.ClientSideEvents.GotFocus = "function(s, e) { s.ShowDropDown(); }"; }
            if (dropdownWidth.HasValue) comboBoxSettings.Properties.DropDownWidth = Unit.Pixel(dropdownWidth.Value);
            if (dataSource != null) comboBoxSettings.Properties.DataSource = dataSource;
            comboBoxSettings.ControlStyle.CssClass = CssClasses.Editor;
            comboBoxSettings.Properties.TextField = textField;
            comboBoxSettings.Properties.ValueField = valueField;
            comboBoxSettings.Properties.ValueType = valueType == null ? typeof(string) : valueType;
            if (valueType == typeof(int) || valueType == typeof(short) || valueType == typeof(long) || valueType == typeof(byte))
            {
                comboBoxSettings.Properties.ClientSideEvents.Init = Strings.FunctionIntComboBoxInit;
            }
            if (!string.IsNullOrWhiteSpace(comboBoxSettings.Name) && clientSideEvents.Length > 0)
            {
                clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => comboBoxSettings.Properties.ClientSideEvents.SetEventHandler(e, comboBoxSettings.Name.Add(e)));
            }
            if (displayColumns != null && displayColumns.Length > 1)
            {
                comboBoxSettings.Properties.TextFormatString = textFormatString;
                displayColumns.ToList().ForEach(c => comboBoxSettings.Properties.Columns.Add(c));
            }
            if (!string.IsNullOrWhiteSpace(helpText))
            {
                comboBoxSettings.Properties.HelpText = helpText;
                comboBoxSettings.Properties.HelpTextSettings.DisplayMode = helpTextDisplayMode;
                if (helpTextDisplayMode == HelpTextDisplayMode.Popup)
                {
                    comboBoxSettings.Properties.HelpTextSettings.EnablePopupAnimation = true;
                }
                else if (helpTextDisplayMode == HelpTextDisplayMode.Inline)
                {
                    comboBoxSettings.Properties.HelpTextSettings.Position = HelpTextPosition.Bottom;
                }

            }
            comboBoxSettings.Properties.ClearButton.DisplayMode = clearButtonDisplayMode;
            comboBoxSettings.ShowModelErrors = showModelErrors;
            comboBoxSettings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
            comboBoxSettings.ClientEnabled = enabled;
        }
        #endregion

        #region Button

        /// <summary>
        /// Forms the layout button settings.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <param name="useSubmitBehavior">if set to <c>true</c> [use submit behavior].</param>
        /// <param name="clientSideEventClickFunction">The client side event click function.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues"></param>
        public static void FormLayoutButtonSettings(MVCxFormLayoutItem button, string text,
            string name = null, bool useSubmitBehavior = false,
            string clientSideEventClickFunction = null,
            string controller = null, string action = null, object routeValues = null)
        {
            button.Caption = string.Empty;
            button.NestedExtensionType = FormLayoutNestedExtensionItemType.Button;
            ButtonSettings buttonSettings = (ButtonSettings)button.NestedExtensionSettings;
            ButtonSettings(buttonSettings,
                text: text,
                name: name,
                useSubmitBehavior: useSubmitBehavior,
                clientSideEventClickFunction: clientSideEventClickFunction,
                controller: controller,
                action: action,
                routeValues: routeValues);
        }

        /// <summary>
        /// Buttons the settings.
        /// </summary>
        /// <param name="buttonSettings">The button settings.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <param name="useSubmitBehavior">if set to <c>true</c> [use submit behavior].</param>
        /// <param name="clientSideEventClickFunction">The client side event click function.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="width"></param>
        /// <param name="cssClass"></param>
        public static void ButtonSettings(ButtonSettings buttonSettings, string text,
            string name = null, bool useSubmitBehavior = false,
            string clientSideEventClickFunction = null,
            string controller = null, string action = null, object routeValues = null, int? width = null, string cssClass = null, bool clientEnabled = true)
        {
            buttonSettings.ControlStyle.CssClass = $"{CssClasses.Button} {cssClass}";
            buttonSettings.Name = string.IsNullOrWhiteSpace(name) ? text.Replace(" ", string.Empty) : name;
            buttonSettings.Text = text;
            buttonSettings.ClientEnabled = clientEnabled;
            if (width.HasValue)
            {
                buttonSettings.Width = Unit.Pixel(width.Value);
            }
            buttonSettings.UseSubmitBehavior = useSubmitBehavior;
            if (!useSubmitBehavior)
            {
                if (clientSideEventClickFunction != null)
                {
                    buttonSettings.ClientSideEvents.Click = clientSideEventClickFunction;
                }
                else
                {
                    if (routeValues != null)
                    {
                        buttonSettings.ClientSideEvents.Click = "function(s, e){ document.location='" + DevExpressHelper.GetUrl(routeValues) + "'; }";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action))
                        {
                            buttonSettings.ClientSideEvents.Click = "function(s, e){ document.location='" +
                                                                    DevExpressHelper.GetUrl(
                                                                        new { Controller = controller, Action = action }) +
                                                                    "'; }";
                        }
                    }
                }
            }
        }
        #endregion

        #region Submit button

        /// <summary>
        /// Forms the layout submit button settings.
        /// </summary>
        /// <param name="submitButton">The submit button.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        public static void FormLayoutSubmitButtonSettings(MVCxFormLayoutItem submitButton, string text, string name = null)
        {
            FormLayoutButtonSettings(submitButton, text: text, name: name, useSubmitBehavior: true);
        }

        /// <summary>
        /// Submits the button settings.
        /// </summary>
        /// <param name="buttonSettings">The button settings.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        public static void SubmitButtonSettings(ButtonSettings buttonSettings, string text, string name = null)
        {
            ButtonSettings(buttonSettings, text: text, name: name, useSubmitBehavior: true);
        }
        #endregion

        #region Cancel button

        /// <summary>
        /// Forms the layout cancel button settings.
        /// </summary>
        /// <param name="cancelButton">The cancel button.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <param name="useSubmitBehavior">if set to <c>true</c> [use submit behavior].</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues"></param>
        public static void FormLayoutCancelButtonSettings(MVCxFormLayoutItem cancelButton, string text,
            string name = null, string controller = null, string action = null, object routeValues = null)
        {
            FormLayoutButtonSettings(cancelButton,
                text: text,
                name: name,
                useSubmitBehavior: false,
                controller: controller,
                action: action,
                routeValues: routeValues);
        }

        /// <summary>
        /// Cancels the button settings.
        /// </summary>
        /// <param name="buttonSettings">The button settings.</param>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues"></param>
        public static void CancelButtonSettings(ButtonSettings buttonSettings, string text,
            string name = null, string controller = null, string action = null, object routeValues = null, string cssClass = null)
        {
            buttonSettings.CausesValidation = false;
            ButtonSettings(buttonSettings,
                text: text,
                name: name,
                useSubmitBehavior: false,
                controller: controller,
                action: action,
                routeValues: routeValues,
                cssClass: cssClass);
        }
        #endregion

        #region ImageEdit
        /// <summary>
        /// Forms the layout image edit settings.
        /// </summary>
        /// <param name="imageEdit">The image edit.</param>
        /// <param name="name">The name.</param>
        /// <param name="alternateText">The alternate text.</param>
        public static void FormLayoutImageEditSettings(MVCxFormLayoutItem imageEdit, string name, string alternateText = null)
        {
            imageEdit.NestedExtensionType = FormLayoutNestedExtensionItemType.Image;
            ImageEditSettings imageEditSettings = (ImageEditSettings)imageEdit.NestedExtensionSettings;
            ImageEditSettings(imageEditSettings, name, alternateText);
        }

        /// <summary>
        /// Images the edit settings.
        /// </summary>
        /// <param name="imageEditSettings">The image edit settings.</param>
        /// <param name="name">The name.</param>
        /// <param name="alternateText">The alternate text.</param>
        public static void ImageEditSettings(ImageEditSettings imageEditSettings, string name, string alternateText = null)
        {
            imageEditSettings.Name = name;
            imageEditSettings.Properties.SpriteCssClass = CssClasses.Icon;
            imageEditSettings.Properties.AlternateText = string.IsNullOrWhiteSpace(alternateText) ? string.Empty : alternateText;
        }
        #endregion

        #region DateEdit

        /// <summary>
        /// Forms the layout date edit settings.
        /// </summary>
        /// <param name="dateEdit">The date edit.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="colSpan"></param>
        /// <param name="openOnFocus"></param>
        /// <param name="clearButtonDisplayMode"></param>
        /// <param name="minDate"></param>
        /// <param name="clientSideEvents"></param>
        public static void FormLayoutDateEditSettings(MVCxFormLayoutItem dateEdit, string caption = null, int? colSpan = null, bool openOnFocus = true,
            ClearButtonDisplayMode clearButtonDisplayMode = ClearButtonDisplayMode.OnHover, DateTime? minDate = null, DateTime? maxDate = null, bool enabled = true,
            int? width = null,bool showModelErrors= true, params JSEvents[] clientSideEvents)
        {
            if (!string.IsNullOrWhiteSpace(caption)) { dateEdit.Caption = caption; }
            if (colSpan.HasValue) { dateEdit.ColSpan = colSpan.Value; }
            
            dateEdit.NestedExtensionType = FormLayoutNestedExtensionItemType.DateEdit;
            DateEditSettings dateEditSettings = (DateEditSettings)dateEdit.NestedExtensionSettings;
            dateEditSettings.ControlStyle.CssClass = CssClasses.Editor;
            dateEditSettings.ShowModelErrors = true;
            if (width.HasValue) dateEditSettings.Width = width.Value;
            dateEditSettings.Properties.MinDate = minDate ?? DateTime.MinValue;
            dateEditSettings.Properties.MaxDate = maxDate ?? DateTime.MaxValue;
            dateEditSettings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
            dateEditSettings.Properties.CalendarProperties.ShowWeekNumbers = false;
            dateEditSettings.ShowModelErrors = showModelErrors;
            if (openOnFocus) { dateEditSettings.Properties.ClientSideEvents.GotFocus = "function(s, e) { s.ShowDropDown(); }"; }
            dateEditSettings.Properties.ClearButton.DisplayMode = clearButtonDisplayMode;
            if (!string.IsNullOrWhiteSpace(dateEditSettings.Name) && clientSideEvents.Length > 0)
            {
                clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => dateEditSettings.Properties.ClientSideEvents.SetEventHandler(e, dateEditSettings.Name.Add(e)));
            }
            dateEditSettings.ClientEnabled = enabled;
        }
        #endregion

        #region TokenBox

        /// <summary>
        /// Forms the layout token box settings.
        /// </summary>
        /// <param name="tokenBox">The token box.</param>
        /// <param name="textField">The text field.</param>
        /// <param name="valueField">The value field.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="allowCustomTokens">if set to <c>true</c> [allow custom tokens].</param>
        /// <param name="colSpan"></param>
        /// <param name="incrementalFilteringMode">The incremental filtering mode.</param>
        /// <param name="showDropDownOnFocusMode">The show drop down on focus mode.</param>
        public static void FormLayoutTokenBoxSettings(MVCxFormLayoutItem tokenBox, object dataSource,
            string textField = DbColumns.Libelle, string valueField = DbColumns.Id, int? widthPixel = null,
            string caption = null, bool allowCustomTokens = false, int? colSpan = null,
            IncrementalFilteringMode incrementalFilteringMode = IncrementalFilteringMode.Contains,
            ShowDropDownOnFocusMode showDropDownOnFocusMode = ShowDropDownOnFocusMode.Always)
        {
            if (!string.IsNullOrWhiteSpace(caption)) { tokenBox.Caption = caption; }
            if (colSpan.HasValue) { tokenBox.ColSpan = colSpan.Value; }
            tokenBox.NestedExtensionType = FormLayoutNestedExtensionItemType.TokenBox;
            TokenBoxSettings tokenBoxSettings = (TokenBoxSettings)tokenBox.NestedExtensionSettings;
            TokenBoxSettings(tokenBoxSettings,
                textField: textField,
                valueField: valueField,
                dataSource: dataSource,
                widthPixel: widthPixel,
                allowCustomTokens: allowCustomTokens,
                incrementalFilteringMode: incrementalFilteringMode,
                showDropDownOnFocusMode: showDropDownOnFocusMode);
        }

        /// <summary>
        /// Tokens the box settings.
        /// </summary>
        /// <param name="tokenBoxSettings">The token box settings.</param>
        /// <param name="textField">The text field.</param>
        /// <param name="valueField">The value field.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="name"></param>
        /// <param name="allowCustomTokens">if set to <c>true</c> [allow custom tokens].</param>
        /// <param name="incrementalFilteringMode">The incremental filtering mode.</param>
        /// <param name="showDropDownOnFocusMode">The show drop down on focus mode.</param>
        public static void TokenBoxSettings(TokenBoxSettings tokenBoxSettings, string textField, string valueField,
            object dataSource = null, string name = null, string caption = null, int? widthPixel = null, bool allowCustomTokens = false,
            IncrementalFilteringMode incrementalFilteringMode = IncrementalFilteringMode.Contains,
            ShowDropDownOnFocusMode showDropDownOnFocusMode = ShowDropDownOnFocusMode.Always)
        {
            tokenBoxSettings.ControlStyle.CssClass = CssClasses.Editor;
            tokenBoxSettings.Properties.AllowCustomTokens = allowCustomTokens;
            tokenBoxSettings.Properties.IncrementalFilteringMode = incrementalFilteringMode;
            tokenBoxSettings.Properties.ShowDropDownOnFocus = showDropDownOnFocusMode;
            tokenBoxSettings.Properties.TextField = textField;
            tokenBoxSettings.Properties.ValueField = valueField;

            if (!string.IsNullOrWhiteSpace(name)) tokenBoxSettings.Name = name;
            if (!string.IsNullOrWhiteSpace(caption)) tokenBoxSettings.Properties.Caption = caption;
            if (widthPixel.HasValue) tokenBoxSettings.Width = Unit.Pixel(widthPixel.Value);
            if (dataSource != null) tokenBoxSettings.Properties.DataSource = dataSource;
        }
        #endregion

        #region ImageEdit
        /// <summary>
        /// Images the zoom settings.
        /// </summary>
        /// <param name="imageZoomSettings">The image zoom settings.</param>
        /// <param name="name">The name.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="zoomWidth">Width of the zoom.</param>
        /// <param name="zoomHeight">Height of the zoom.</param>
        public static void ImageZoomSettings(ImageZoomSettings imageZoomSettings,
            string name, string imageUrl, int width = 30, int height = 30,
            int zoomWidth = 450, int zoomHeight = 450)
        {
            imageZoomSettings.Name = name;
            imageZoomSettings.ImageUrl = imageUrl;
            imageZoomSettings.Width = Unit.Pixel(width);
            imageZoomSettings.Height = Unit.Pixel(height);
            imageZoomSettings.SettingsZoomMode.ZoomWindowHeight = Unit.Pixel(zoomHeight);
            imageZoomSettings.SettingsZoomMode.ZoomWindowWidth = Unit.Pixel(zoomWidth);
            imageZoomSettings.LargeImageUrl = imageUrl;
            imageZoomSettings.EnableExpandMode = true;
            imageZoomSettings.ShowHint = false;
            imageZoomSettings.LargeImageLoadMode = LargeImageLoadMode.Direct;
            imageZoomSettings.SettingsZoomMode.ZoomWindowPosition = ZoomWindowPosition.Right;
            imageZoomSettings.SettingsZoomMode.MouseBoxOpacityMode = MouseBoxOpacityMode.Outside;
        }
        #endregion

        #region DropDownEdit
        public static void DropDownEditSettings(DropDownEditSettings dropDownEditSettings, string name = null, int width = 200, params JSEvents[] clientSideEvents)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                dropDownEditSettings.Name = name;
            }

            if (!string.IsNullOrWhiteSpace(dropDownEditSettings.Name) && clientSideEvents.Length > 0)
            {
                clientSideEvents.Select(e => e.ToString()).ToList().ForEach(e => dropDownEditSettings.Properties.ClientSideEvents.SetEventHandler(e, dropDownEditSettings.Name.Add(e)));
            }

            dropDownEditSettings.Width = width;
        }
        #endregion
        #endregion

        #region GridView
        /// <summary>
        /// Grids the view column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="toolTip">The tool tip.</param>
        /// <param name="minWidth">The minimum width.</param>
        /// <param name="displayFormatString"></param>
        /// <param name="cbxDataSource">The CBX data source.</param>
        /// <param name="cbxTextField">The CBX text field.</param>
        /// <param name="cbxValueField">The CBX value field.</param>
        /// <param name="cbxValueType">Type of the CBX value.</param>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <param name="width"></param>
        /// <param name="visible"></param>
        /// <param name="horizontalAlign"></param>
        private static void GridViewColumn(MVCxGridViewColumn column, string fieldName = null,
            MVCxGridViewColumnType columnType = MVCxGridViewColumnType.Default, bool openOnFocus = true,
            string caption = null, string toolTip = null, int? minWidth = null, int? width = null, string displayFormatString = null,
            object cbxDataSource = null, string cbxTextField = DbColumns.Libelle, string cbxValueField = DbColumns.Id, Type cbxValueType = null,
            SinbaDateTimeFormat dateTimeFormat = SinbaDateTimeFormat.Date, bool visible = true, HorizontalAlign horizontalAlign = HorizontalAlign.NotSet,
            SpinEditNumberType numberType = SpinEditNumberType.Float, int decimalPlaces = 2, decimal increment = 0, bool showIncrementButtons = false, decimal minValue = 0, decimal maxValue = 100000)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                column.FieldName = fieldName;
            }
            column.Visible = visible;
            if (horizontalAlign != HorizontalAlign.NotSet)
            {
                column.CellStyle.HorizontalAlign = horizontalAlign;
            }
            if (minWidth.HasValue) { column.MinWidth = minWidth.Value; }
            if (width.HasValue) { column.Width = Unit.Pixel(width.Value); }
            if (!string.IsNullOrWhiteSpace(caption)) { column.Caption = caption; }
            column.ToolTip = string.IsNullOrWhiteSpace(toolTip) ? string.IsNullOrWhiteSpace(caption) ? null : caption : toolTip;
            column.ColumnType = columnType;
            switch (columnType)
            {
                case MVCxGridViewColumnType.ComboBox:
                    var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
                    comboBoxProperties.DataSource = cbxDataSource;
                    comboBoxProperties.TextField = string.IsNullOrWhiteSpace(cbxTextField) ? DbColumns.Libelle : cbxTextField;
                    comboBoxProperties.ValueField = string.IsNullOrWhiteSpace(cbxValueField) ? DbColumns.Id : cbxValueField;
                    comboBoxProperties.ValueType = cbxValueType ?? typeof(int);
                    
                    if (openOnFocus)
                    {
                        comboBoxProperties.ClientSideEvents.GotFocus = "function(s, e) { s.ShowDropDown(); }";
                    }
                    break;
                case MVCxGridViewColumnType.DateEdit:
                    switch (dateTimeFormat)
                    {
                        case SinbaDateTimeFormat.Date:
                            column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
                            break;
                        case SinbaDateTimeFormat.DayMonth:
                            column.PropertiesEdit.DisplayFormatString = "dd/MM";
                            break;
                        case SinbaDateTimeFormat.Year:
                            column.PropertiesEdit.DisplayFormatString = "yyyy";
                            break;
                    }
                    break;
                case MVCxGridViewColumnType.SpinEdit:
                    var spinEditProperties = column.PropertiesEdit as SpinEditProperties;
                    spinEditProperties.DecimalPlaces = decimalPlaces;
                    spinEditProperties.NumberType = numberType;
                    spinEditProperties.SpinButtons.ClientVisible = false;
                    column.EditorProperties().SpinEdit(editor =>
                    {
                        if (!string.IsNullOrWhiteSpace(displayFormatString))
                        {
                            editor.DisplayFormatString = displayFormatString;
                        }
                        editor.DecimalPlaces = decimalPlaces;
                        editor.Increment = increment;
                        editor.SpinButtons.ShowIncrementButtons = showIncrementButtons;
                        editor.MinValue = minValue;
                        editor.MaxValue = maxValue;
                        editor.NumberType = numberType;
                    });
                    break;
                case MVCxGridViewColumnType.CheckBox:

                    break;
                case MVCxGridViewColumnType.Default:
                    if (!string.IsNullOrWhiteSpace(displayFormatString)) { column.PropertiesEdit.DisplayFormatString = displayFormatString; }
                    break;
            }
        }

        /// <summary>
        /// Grids the view default column.
        /// </summary>
        /// <param name="textColumn">The text column.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="displayFormatString"></param>
        /// <param name="caption">The caption.</param>
        /// <param name="toolTip">The tool tip.</param>
        /// <param name="minWidth">The minimum width.</param>
        /// <param name="visible"></param>
        /// <param name="horizontalAlign"></param>
        public static void GridViewDefaultColumn(MVCxGridViewColumn textColumn, string fieldName = null,
            MVCxGridViewColumnType columnType = MVCxGridViewColumnType.Default, string displayFormatString = null, int? width = null,
            string caption = null, string toolTip = null, int? minWidth = null, bool visible = true, HorizontalAlign horizontalAlign = HorizontalAlign.NotSet)
        {
            GridViewColumn(textColumn, fieldName,
                caption: caption,
                toolTip: toolTip,
                minWidth: minWidth,
                width: width,
                columnType: columnType,
                displayFormatString: displayFormatString,
                visible: visible,
                horizontalAlign: horizontalAlign);
        }

        public static void GridViewComboBoxColumn(MVCxGridViewColumn comboBoxColumn, string fieldName = null,
            string caption = null, string toolTip = null, int? minWidth = null, int? width = null, bool openOnFocus = true,
            object dataSource = null, string textField = DbColumns.Libelle, string valueField = DbColumns.Id, Type valueType = null, bool visible = true)
        {
            GridViewColumn(comboBoxColumn, fieldName, MVCxGridViewColumnType.ComboBox,
                caption: caption,
                toolTip: toolTip,
                minWidth: minWidth,
                width: width,
                openOnFocus: openOnFocus,
                cbxDataSource: dataSource,
                cbxTextField: textField,
                cbxValueField: valueField,
                cbxValueType: valueType,
                visible: visible);
        }

        public static void GridViewSpinEditColumn(MVCxGridViewColumn comboBoxColumn, string fieldName = null,
            string caption = null, SpinEditNumberType numberType = SpinEditNumberType.Float, int decimalPlaces = 2, decimal increment = 0, bool showIncrementButtons = false,
            string toolTip = null, int? minWidth = null, int? width = null, bool visible = true, string displayFormatString = "{0:0.##}", decimal minValue = 0, decimal maxValue = 100000)
        {
            GridViewColumn(comboBoxColumn,
                fieldName,
                MVCxGridViewColumnType.SpinEdit,
                numberType: numberType,
                decimalPlaces: decimalPlaces,
                caption: caption,
                toolTip: toolTip,
                minWidth: minWidth,
                width: width,
                increment: increment,
                minValue: minValue,
                maxValue: maxValue,
                displayFormatString: displayFormatString,
                showIncrementButtons: showIncrementButtons,
                visible: visible);
        }

        /// <summary>
        /// Grids the view date column.
        /// </summary>
        /// <param name="dateColumn">The date column.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="toolTip">The tool tip.</param>
        /// <param name="minWidth">The minimum width.</param>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <param name="visible"></param>
        public static void GridViewDateColumn(MVCxGridViewColumn dateColumn, string fieldName = null,
            string caption = null, string toolTip = null, int? minWidth = null, int? width = null,
            SinbaDateTimeFormat dateTimeFormat = SinbaDateTimeFormat.Date, bool visible = true)
        {
            GridViewColumn(dateColumn, fieldName, MVCxGridViewColumnType.DateEdit,
                caption: caption,
                toolTip: toolTip,
                minWidth: minWidth,
                width: width,
                dateTimeFormat: dateTimeFormat,
                visible: visible);
        }

        public static void GridViewCheckboxColumn(MVCxGridViewColumn checkboxColumn, string caption = null, bool displayCaption = true)
        {
            GridViewColumn(checkboxColumn,
                caption: caption,
                columnType: MVCxGridViewColumnType.CheckBox);
            checkboxColumn.Caption = caption;
        }

        public static void GridViewDefaultSettings(GridViewSettings settings, string name, string[] keyFieldName = null,
            object callbackRouteValues = null, bool showFilter = true, AutoFilterCondition autoFilterCondition = AutoFilterCondition.Contains,
            bool enablePaging = true, string masterGridName = null, bool showDetailRow = false, bool allowSortResize = true, bool allowDragDrop = true, int? width = null,
            ColumnResizeMode columnResizeMode = ColumnResizeMode.Disabled, HorizontalAlign headerHorizontalAlign = HorizontalAlign.NotSet, string emptyDataRow = null,
            GridViewEditingMode gridViewEditingMode = GridViewEditingMode.EditFormAndDisplayRow, bool highlightDeletedRows = false,
            bool showFilterRowMenu = false)
        {
            settings.Name = name;
            if (!string.IsNullOrWhiteSpace(masterGridName)) { settings.SettingsDetail.MasterGridName = masterGridName; }
            if (keyFieldName != null) settings.KeyFieldName = string.Join(Strings.Semicolon, keyFieldName);
            if (width.HasValue) settings.Width = width.Value;
            if (callbackRouteValues != null) { settings.CallbackRouteValues = callbackRouteValues; }
            if (showFilter)
            {
                settings.Settings.ShowFilterRow = true;
                settings.Settings.ShowFilterRowMenu = showFilterRowMenu;
                settings.Settings.AutoFilterCondition = autoFilterCondition;
            }

            settings.SettingsDetail.ShowDetailRow = showDetailRow;
            settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = false;
            settings.Styles.Header.HorizontalAlign = headerHorizontalAlign;
            settings.SettingsBehavior.AllowDragDrop = allowDragDrop;
            settings.SettingsBehavior.AllowSort = allowSortResize;
            if (!allowSortResize)
            {
                settings.ClientSideEvents.ColumnSorting = "function(s, e) { e.cancel = true; }";
                //settings.ClientSideEvents.ColumnResizing = "function(s, e) { e.cancel = true; }";
            }

            // Pagination
            settings.SettingsPager.FirstPageButton.Visible = enablePaging;
            settings.SettingsPager.LastPageButton.Visible = enablePaging;
            settings.SettingsPager.PageSizeItemSettings.Visible = enablePaging;

            // Disable command column resizing
            if (columnResizeMode != ColumnResizeMode.Disabled)
            {
                settings.ClientSideEvents.ColumnResizing = "function(s, e) { if(e.column.name == 'cmd') e.cancel = true; }";
            }

            settings.SettingsEditing.Mode = gridViewEditingMode;
            if (gridViewEditingMode == GridViewEditingMode.Batch)
            {
                settings.SettingsEditing.BatchEditSettings.EditMode = BatchEditingHelper.BatchEditMode;
                settings.SettingsEditing.BatchEditSettings.StartEditAction = BatchEditingHelper.BatchStartEditAction;
                settings.SettingsText.EmptyDataRow = emptyDataRow == null ? Strings.EmptyLink : emptyDataRow;
                settings.SettingsEditing.BatchEditSettings.ShowConfirmOnLosingChanges = false;
                settings.SettingsEditing.BatchEditSettings.HighlightDeletedRows = highlightDeletedRows;
                settings.Styles.BatchEditModifiedCell.BackColor = Color.Transparent;
                settings.Styles.BatchEditNewRow.BackColor = Color.Transparent;
            }

            // Default
            settings.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
            settings.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
            settings.Styles.Header.Font.Bold = true;
        }

        public static string DeleteJS(string gridName, GridViewDataItemTemplateContainer container, string controllerName, string actionName = null)
        {
            if (string.IsNullOrWhiteSpace(actionName)) { actionName = SinbaConstants.Actions.Delete; }
            var requestContext = System.Web.HttpContext.Current.Request.RequestContext;
            string url = new UrlHelper(requestContext).Action(actionName, controllerName).ToString();
            return string.Format("return deleteGridRow({0}, '{1}', '{2}')", container?.KeyValue, gridName, url);
        }
        #endregion

        #region Report
        public static void DocumentViewer(DocumentViewerSettings documentViewSettings, XtraReport report, string controllerName, int? width = 1200)
        {
            documentViewSettings.Name = "DocumentViewer";
            documentViewSettings.Report = report;
            documentViewSettings.Width = width.HasValue ? width.Value : 1200;
            documentViewSettings.StylesSplitter.SidePaneMinWidth = 275;
            documentViewSettings.SettingsSplitter.SidePaneVisible = true;
            documentViewSettings.CallbackRouteValues = new { Controller = controllerName, Action = SinbaConstants.Actions.DocumentViewerPartial };
            documentViewSettings.ExportRouteValues = new { Controller = controllerName, Action = SinbaConstants.Actions.ExportReport };

            var formats = documentViewSettings.ToolbarItems.First(x => x.ItemKind == ReportToolbarItemKind.SaveFormat) as ReportToolbarComboBox;
            formats.Elements.Clear();
            formats.Elements.AddRange(new ListElement[] {
                    new ListElement { Value = "pdf", Text = "PDF"},
                    new ListElement { Value = "xlsx", Text = "Excel"},
                    new ListElement { Value = "png", Text = "Image"}
                });
        }

        public static void WebDocumentViewer(WebDocumentViewerSettings webDocumentViewerSettings, int? width = null, string beginCallBack = null)
        {
            webDocumentViewerSettings.Name = "WebDocumentViewer";
            webDocumentViewerSettings.Width = width.HasValue ? width.Value : 1200;
            webDocumentViewerSettings.ClientSideEvents.BeginCallback = beginCallBack;
        }
        #endregion

        #region Popup
        public static void PopupSettings(PopupControlSettings popupControlSettings, string name, string headerText = null, int? minWidth = null, bool allowDragging = true, bool modal = true, bool closeOnEscape = true, CloseAction closeAction = CloseAction.CloseButton, PopupHorizontalAlign horizontalAlign = PopupHorizontalAlign.WindowCenter, PopupVerticalAlign verticalAlign = PopupVerticalAlign.WindowCenter, string clientSideEventsCloseUp = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                popupControlSettings.Name = name;
            }
            if (!string.IsNullOrEmpty(clientSideEventsCloseUp)) popupControlSettings.ClientSideEvents.CloseUp = clientSideEventsCloseUp;

            popupControlSettings.MinWidth = minWidth.HasValue ? minWidth.Value : Unit.Percentage(100);
            popupControlSettings.HeaderText = headerText;
            popupControlSettings.AllowDragging = allowDragging;
            popupControlSettings.Modal = modal;
            popupControlSettings.CloseAction = closeAction;
            popupControlSettings.CloseOnEscape = closeOnEscape;
            popupControlSettings.PopupHorizontalAlign = horizontalAlign;
            popupControlSettings.PopupVerticalAlign = verticalAlign;
            popupControlSettings.PopupAnimationType = AnimationType.Fade;
            popupControlSettings.AppearAfter = 10000;
            }
        #endregion

        #region Model
        public static string GetCompositeFieldName(params string[] fields)
        {
            return string.Join(".", fields);
        }
        #endregion
        #endregion
    }
}
