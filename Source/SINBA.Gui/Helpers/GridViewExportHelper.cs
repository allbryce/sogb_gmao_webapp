using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using Sinba.Gui.Resources;
using Sinba.Gui.UIModels;
using Sinba.Resources;
using Sinba.Resources.Resources.Entity;
using DevExpress.Export;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using System.Collections;
using System.Web.UI.WebControls;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web.Mvc;

namespace Sinba.Gui.Helpers
{
    public enum GridViewExportFormat { None, Pdf, Xls, Xlsx, Rtf, Csv }
    public delegate ActionResult GridViewExportMethod(GridViewSettings settings, object dataObject);

    public enum PivotGridExportFormats { None, Pdf, Xls, Xlsx, Rtf, Csv }

    //public enum PivotGridExportFormats { None,ExcelDataAware, Pdf, Excel, Mht, Rtf, Text, Html }
    public class GridViewExportHelper
    {
#pragma warning disable CS0414 // Le champ 'GridViewExportHelper.ExcelDataAwareGridViewSettingsKey' est assigné, mais sa valeur n'est jamais utilisée
        static string ExcelDataAwareGridViewSettingsKey = "51172A18-2073-426B-A5CB-136347B3A79F";
#pragma warning restore CS0414 // Le champ 'GridViewExportHelper.ExcelDataAwareGridViewSettingsKey' est assigné, mais sa valeur n'est jamais utilisée
#pragma warning disable CS0414 // Le champ 'GridViewExportHelper.FormatConditionsExportGridViewSettingsKey' est assigné, mais sa valeur n'est jamais utilisée
        static string FormatConditionsExportGridViewSettingsKey = "14634B6F-E1DC-484A-9728-F9608615B628";
#pragma warning restore CS0414 // Le champ 'GridViewExportHelper.FormatConditionsExportGridViewSettingsKey' est assigné, mais sa valeur n'est jamais utilisée

        static Dictionary<GridViewExportFormat, GridViewExportMethod> exportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> ExportFormatsInfo
        {
            get
            {
                if (exportFormatsInfo == null)
                    exportFormatsInfo = CreateExportFormatsInfo();
                return exportFormatsInfo;
            }
        }

        static IDictionary Context { get { return System.Web.HttpContext.Current.Items; } }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf },
                {
                    GridViewExportFormat.Xls,
                    (settings, data) => GridViewExtension.ExportToXls(settings, data, new XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                {
                    GridViewExportFormat.Xlsx,
                    (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf },
                {
                    GridViewExportFormat.Csv,
                    (settings, data) => GridViewExtension.ExportToCsv(settings, data, new CsvExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> dataAwareExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> DataAwareExportFormatsInfo
        {
            get
            {
                if (dataAwareExportFormatsInfo == null)
                    dataAwareExportFormatsInfo = CreateDataAwareExportFormatsInfo();
                return dataAwareExportFormatsInfo;
            }
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateDataAwareExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Xls, GridViewExtension.ExportToXls },
                { GridViewExportFormat.Xlsx, GridViewExtension.ExportToXlsx },
                { GridViewExportFormat.Csv, GridViewExtension.ExportToCsv }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> formatConditionsExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> FormatConditionsExportFormatsInfo
        {
            get
            {
                if (formatConditionsExportFormatsInfo == null)
                    formatConditionsExportFormatsInfo = CreateFormatConditionsExportFormatsInfo();
                return formatConditionsExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateFormatConditionsExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf},
                { GridViewExportFormat.Xls, (settings, data) => GridViewExtension.ExportToXls(settings, data) },
                { GridViewExportFormat.Xlsx,
                    (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG})
                },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> advancedBandsExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> AdvancedBandsExportFormatsInfo
        {
            get
            {
                if (advancedBandsExportFormatsInfo == null)
                    advancedBandsExportFormatsInfo = CreateAdvancedBandsExportFormatsInfo();
                return advancedBandsExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateAdvancedBandsExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf },
                { GridViewExportFormat.Xls, (settings, data) => GridViewExtension.ExportToXls(settings, data, new XlsExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG}) },
                { GridViewExportFormat.Xlsx, (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG}) },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf }
            };
        }

        public static string GetExportButtonTitle(GridViewExportFormat format)
        {
            if (format == GridViewExportFormat.None)
                return string.Empty;
            return string.Format("Export to {0}", format.ToString().ToUpper());
        }

        static GridViewSettings exportGridViewSettings;
        public static GridViewSettings ExportGridViewSettings
        {
            get
            {
                if (exportGridViewSettings == null)
                    exportGridViewSettings = ExportBudget();
                return exportGridViewSettings;
            }
        }
        static GridViewSettings ExportBudget()
        {
            GridViewSettings settings = new GridViewSettings();


            // Pagination
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;

            settings.Name = "gridConsultationBudget";
            //settings.KeyFieldName = string.Format("{0};{1};{2};{3}", DbColumns.CodeVersionBudget, DbColumns.CodePeriode, DbColumns.IdRubriqueGroupe, DbColumns.IdDestination);
            settings.CallbackRouteValues = new { Controller = SinbaConstants.Controllers.Contact, Action = SinbaConstants.Actions.ListPartial };
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.Styles.Header.Font.Bold = true;
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.Width = Unit.Percentage(100);

            // Pagination
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.ClientSideEvents.BeginCallback = "OnBeginCallback";

            // CodeVersionBudget
            //settings.Columns.Add(DbColumns.CodeVersionBudget, EntityColumnResource.CodeVersionBudget);

            //// CodePeriode
            //settings.Columns.Add(DbColumns.CodePeriode, EntityColumnResource.CodePeriode);

            //// LibelleRubriqueGroupe
            //settings.Columns.Add(DbColumns.LibelleRubriqueGroupe, EntityColumnResource.Rubrique);

            //// LibelleDestination
            //settings.Columns.Add(DbColumns.LibelleDestination, EntityColumnResource.LibelleDestination);

            //// nbuo
            //settings.Columns.Add(DbColumns.nbuo, EntityColumnResource.nbuo);

            //// Montant
            //settings.Columns.Add(DbColumns.Montant, EntityColumnResource.Montant);

            //// LibelleNature
            //settings.Columns.Add(DbColumns.LibelleNature, EntityColumnResource.LibelleNature);

            //// LibelleCentreDeCout
            //settings.Columns.Add(DbColumns.LibelleCentreDeCout, EntityColumnResource.CentreCout);

            //// LibelleService
            //settings.Columns.Add(DbColumns.LibelleService, EntityColumnResource.Service);

            //// LibelleDepartement
            //settings.Columns.Add(DbColumns.LibelleDepartement, EntityColumnResource.Departement);

            //// LibelleDirection
            //settings.Columns.Add(DbColumns.LibelleDirection, EntityColumnResource.Direction);

            //// LibelleDirection
            //settings.Columns.Add(DbColumns.IdRubriqueGroupe, EntityColumnResource.IdRubriqueGroupe);

            settings.SettingsExport.RenderBrick = (sender, e) =>
            {
                if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
                    e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            };


            return settings;
        }
        public static GridViewSettings ExportGridView(string Name, string KeyFieldName, string CallbackController, string CallbackAction, List<ColumnObjet> Columns)
        {

            var settings = new GridViewSettings();

            settings.Name = Name;
            settings.KeyFieldName = KeyFieldName;
            settings.CallbackRouteValues = new { Controller = CallbackController, Action = CallbackAction };
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.Styles.Header.Font.Bold = true;
            settings.Width = Unit.Percentage(100);
            settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            // Pagination
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSize = 10;
            settings.ClientSideEvents.BeginCallback = "OnBeginCallback";
            settings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.LeftMargin = 50;
            settings.SettingsExport.MaxColumnWidth = 90;
            settings.SettingsExport.RightMargin = 0;

            settings.Settings.ShowGroupPanel = true;
            settings.SettingsContextMenu.Enabled = true;
            settings.SettingsContextMenu.EnableColumnMenu = DefaultBoolean.True;
            //Ajouter les colonnes.
            Columns.ForEach(col =>
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = col.Name;
                    column.Width = col.Width;
                    if (col.Name.Contains(DbColumns.Libelle))
                    {
                        column.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                    column.Caption = col.Caption;
                    column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                    column.FixedStyle = GridViewColumnFixedStyle.Left;
                    column.ColumnType = MVCxGridViewColumnType.SpinEdit;
                    column.ReadOnly = true;
                    column.Settings.ShowEditorInBatchEditMode = false;
                    column.EditorProperties().SpinEdit(editor =>
                    {
                        editor.SpinButtons.ShowIncrementButtons = false;
                        editor.MinValue = 0;
                        editor.DisplayFormatString = "n2";
                    });
                });
            });



            //background color for rows
            settings.SettingsExport.RenderBrick = (sender, e) =>
            {
                if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
                    e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            };


            return settings;
        }

        public class ColumnObjet : GridViewColumn
        {
            //public string Name { get; set; }
            //public string Caption { get; set; }
        }



        public static GridViewSettings ExportGridView1(string Name, string KeyFieldName, string CallbackController, string CallbackAction)
        {

            var settings = new GridViewSettings();

            settings.Name = Name;
            settings.KeyFieldName = KeyFieldName;
            settings.CallbackRouteValues = new { Controller = CallbackController, Action = CallbackAction };
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.Styles.Header.Font.Bold = true;
            settings.Width = Unit.Percentage(100);
            settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            
            // Pagination
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSize = 50;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.LeftMargin = 10;
            settings.SettingsExport.MaxColumnWidth = 90;
            settings.SettingsExport.RightMargin = 0;

            settings.Settings.ShowGroupPanel = true;
            settings.SettingsContextMenu.Enabled = true;
            settings.SettingsContextMenu.EnableColumnMenu = DefaultBoolean.True;

            settings.Columns.Add(column =>
            {
                column.Visible = false;
                column.Width = Unit.Pixel(0);
                column.ReadOnly = true;
                column.FixedStyle = GridViewColumnFixedStyle.Left;
                column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            });

            //string[] periodes = { DbColumns.Janvier, DbColumns.Fevrier, DbColumns.Mars, DbColumns.Avril, DbColumns.Mai, DbColumns.Juin, DbColumns.Juillet, DbColumns.Aout, DbColumns.Septembre, DbColumns.Octobre, DbColumns.Novembre, DbColumns.Decembre };

            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = DbColumns.IdArticleEmploi;
            //    column.Caption = EntityColumnResource.IdArticleEmploi;
            //    column.FixedStyle = GridViewColumnFixedStyle.Left;
            //});

            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = DbColumns.LibelleArticleEmploi;
            //    column.Caption = EntityColumnResource.LibelleArticleEmploi;
            //    column.FixedStyle = GridViewColumnFixedStyle.Left;
            //});
            
            //settings.Columns.AddBand(band =>
            //{
            //    band.Caption = DbColumns.Moyenne;
            //    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //    band.FixedStyle = GridViewColumnFixedStyle.Left;
            //    settings.Columns.Add(column =>
            //        {
            //            column.FieldName = DbColumns.MoyenneEffectif;
            //            column.Caption = EntityColumnResource.MoyenneEffectif;
            //            column.FixedStyle = GridViewColumnFixedStyle.Left;

            //        });

            //    settings.Columns.Add(column =>
            //        {
            //            column.FieldName = DbColumns.MoyenneJx;
            //            column.Caption = EntityColumnResource.MoyenneJx;
            //            column.FixedStyle = GridViewColumnFixedStyle.Left;
            //        });
            //});
            
            //settings.Columns.AddBand(band =>
            //{
            //    band.Caption = DbColumns.Total;
            //    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //    band.FixedStyle = GridViewColumnFixedStyle.Left;
            //    settings.Columns.Add(column =>
            //        {
            //            column.FieldName = DbColumns.Total;
            //            column.Caption = EntityColumnResource.Effectif;
            //            column.FixedStyle = GridViewColumnFixedStyle.Left;
            //        });

            //    settings.Columns.Add(column =>
            //    {
            //        column.FieldName = string.Format("{0}_Jx", DbColumns.Total);
            //        column.Caption = EntityColumnResource.Jx;
            //        column.FixedStyle = GridViewColumnFixedStyle.Left;
            //    });
            //    });

            //foreach (var mois in periodes)
            //{

            //    settings.Columns.AddBand(band =>
            //    {
            //        band.Caption = EntityColumnResourceExtension.GetValue(mois);
            //        band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            //        settings.Columns.Add(column =>
            //        {
            //            column.FieldName = mois;
            //            column.Caption = mois + " " + EntityColumnResource.Effectif;
            //        });

            //    settings.Columns.Add(column =>
            //            {
            //                column.FieldName = string.Format("{0}_Jx", mois);
            //                column.Caption = string.Format("{0} Jx", mois);

            //            });

            //     });
            //}
            ////background color for rows
            //settings.SettingsExport.RenderBrick = (sender, e) =>
            //{
            //    if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
            //        e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            //};


            return settings;
        }


    }
    public class  PivotGridViewExportHelper
    {        
        static Dictionary<PivotGridExportFormats, PivotGridExportMethod> exportFormatsInfo;
        public static Dictionary<PivotGridExportFormats, PivotGridExportMethod> ExportFormatsInfo
        {
            get
            {
                if (exportFormatsInfo == null)
                    exportFormatsInfo = CreateExportFormatsInfo();
                return exportFormatsInfo;
            }
        }

        static Dictionary<PivotGridExportFormats, PivotGridExportMethod> CreateExportFormatsInfo()
        {
            return new Dictionary<PivotGridExportFormats, PivotGridExportMethod> {
                { PivotGridExportFormats.Pdf, PivotGridExtension.ExportToPdf },
                {
                    PivotGridExportFormats.Xls,
                    (settings, data) => PivotGridExtension.ExportToXls(settings, data, new XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })

                },
                {
                    PivotGridExportFormats.Xlsx,
                    (settings, data) => PivotGridExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                { PivotGridExportFormats.Rtf, PivotGridExtension.ExportToRtf },
                {
                    PivotGridExportFormats.Csv,
                    (settings, data) => PivotGridExtension.ExportToCsv(settings, data, new CsvExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                }    
            };
        }
        
        public static PivotGridSettings ExportGridView(string Name,  string CallbackController, string CallbackAction ,string budgetCC)
        {
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = Name;
            settings.CallbackRouteValues = new { Controller = CallbackController, Action = CallbackAction };
            settings.Width = Unit.Percentage(100);
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto;

            //switch (budgetCC)
            //{
            //    case "BDCOM1":
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCout;
            //            field.FieldName = DbColumns.IdCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCout;
            //            field.FieldName = DbColumns.LibelleCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 2;
            //            field.Caption = EntityColumnResource.IdSysCulture;
            //            field.FieldName = DbColumns.IdSysCulture;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdSite;
            //            field.FieldName = DbColumns.IdSite;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;

            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
                    
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPP;
            //            field.FieldName = DbColumns.RatioToMPP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPA;
            //            field.FieldName = DbColumns.RatioToMPA;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToUsine;
            //            field.FieldName = DbColumns.RatioToUsine;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        break;

            //    case "BDCOM2":
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCout;
            //            field.FieldName = DbColumns.IdCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCout;
            //            field.FieldName = DbColumns.LibelleCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 2;
            //            field.Caption = EntityColumnResource.IdNature;
            //            field.FieldName = DbColumns.IdNature;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.LibelleNature;
            //            field.FieldName = DbColumns.LibelleNature;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 4;
            //            field.Caption = EntityColumnResource.IdSite;
            //            field.FieldName = DbColumns.IdSite;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
                   
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPP;
            //            field.FieldName = DbColumns.RatioToMPP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPA;
            //            field.FieldName = DbColumns.RatioToMPA;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToUsine;
            //            field.FieldName = DbColumns.RatioToUsine;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        break;
            //    case "BDCOM3":
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCoutNiveau3;
            //            field.FieldName = DbColumns.IdCentreDeCoutNiveau3;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCoutNiveau3;
            //            field.FieldName = DbColumns.LibelleCentreDeCoutNiveau3;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 2;
            //            field.Caption = EntityColumnResource.IdSysCulture;
            //            field.FieldName = DbColumns.IdSysCulture;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdSite;
            //            field.FieldName = DbColumns.IdSite;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;

            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
                    
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPP;
            //            field.FieldName = DbColumns.RatioToMPP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMPA;
            //            field.FieldName = DbColumns.RatioToMPA;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToUsine;
            //            field.FieldName = DbColumns.RatioToUsine;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        break;

            //    case "BDCOM4":

            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCout;
            //            field.FieldName = DbColumns.IdCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCout;
            //            field.FieldName = DbColumns.LibelleCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.LibelleDivision;
            //            field.FieldName = DbColumns.LibelleDivision;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
                    
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });

            //        break;
            //    case "BDCOM5":

            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCout;
            //            field.FieldName = DbColumns.IdCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCout;
            //            field.FieldName = DbColumns.LibelleCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 2;
            //            field.Caption = EntityColumnResource.IdNature;
            //            field.FieldName = DbColumns.IdNature;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.LibelleNature;
            //            field.FieldName = DbColumns.LibelleNature;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 4;
            //            field.Caption = EntityColumnResource.LibelleDivision;
            //            field.FieldName = DbColumns.LibelleDivision;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });

                    
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        break;
            //    case "BDCOM6":
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCoutNiveau3;
            //            field.FieldName = DbColumns.IdCentreDeCoutNiveau3;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCoutNiveau3;
            //            field.FieldName = DbColumns.LibelleCentreDeCoutNiveau3;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.LibelleDivision;
            //            field.FieldName = DbColumns.LibelleDivision;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
                   
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.RatioToSurfExp;
            //            field.FieldName = DbColumns.RatioToSurfExp;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 3;
            //            field.Caption = EntityColumnResource.RatioToMP;
            //            field.FieldName = DbColumns.RatioToMP;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });

            //        break;

            //    case "BDCOM7":
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdCentreDeCout;
            //            field.FieldName = DbColumns.IdCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 1;
            //            field.Caption = EntityColumnResource.LibelleCentreDeCout;
            //            field.FieldName = DbColumns.LibelleCentreDeCout;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.RowArea;
            //            field.AreaIndex = 2;
            //            field.Caption = EntityColumnResource.IdSysCulture;
            //            field.FieldName = DbColumns.IdSysCulture;
            //            field.HeaderStyle.Font.Bold = true;
            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.ColumnArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.IdSite;
            //            field.FieldName = DbColumns.IdSite;
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;

            //        });
            //        settings.Fields.Add(field =>
            //        {
            //            field.Area = PivotArea.DataArea;
            //            field.AreaIndex = 0;
            //            field.Caption = EntityColumnResource.MontantBudget;
            //            field.FieldName = DbColumns.MontantBudget;
            //            field.CellFormat.FormatType = FormatType.Numeric;
            //            field.CellFormat.FormatString = "n2";
            //            field.SortMode = PivotSortMode.None;
            //            field.HeaderStyle.Font.Bold = true;
            //        });

            //        //settings.Fields.Add(field =>
            //        //{
            //        //    field.Area = PivotArea.DataArea;
            //        //    field.AreaIndex = 1;
            //        //    field.Caption = EntityColumnResource.RatioToSurfExp;
            //        //    field.FieldName = DbColumns.RatioToSurfExp;
            //        //    field.CellFormat.FormatType = FormatType.Numeric;
            //        //    field.CellFormat.FormatString = "n2";
            //        //    field.SortMode = PivotSortMode.None;
            //        //    field.HeaderStyle.Font.Bold = true;
            //        //});
            //        //settings.Fields.Add(field =>
            //        //{
            //        //    field.Area = PivotArea.DataArea;
            //        //    field.AreaIndex = 3;
            //        //    field.Caption = EntityColumnResource.RatioToMPP;
            //        //    field.FieldName = DbColumns.RatioToMPP;
            //        //    field.CellFormat.FormatType = FormatType.Numeric;
            //        //    field.CellFormat.FormatString = "n2";
            //        //    field.SortMode = PivotSortMode.None;
            //        //    field.HeaderStyle.Font.Bold = true;
            //        //});
            //        //settings.Fields.Add(field =>
            //        //{
            //        //    field.Area = PivotArea.DataArea;
            //        //    field.AreaIndex = 3;
            //        //    field.Caption = EntityColumnResource.RatioToMP;
            //        //    field.FieldName = DbColumns.RatioToMP;
            //        //    field.CellFormat.FormatType = FormatType.Numeric;
            //        //    field.CellFormat.FormatString = "n2";
            //        //    field.SortMode = PivotSortMode.None;
            //        //    field.HeaderStyle.Font.Bold = true;
            //        //});
            //        //settings.Fields.Add(field =>
            //        //{
            //        //    field.Area = PivotArea.DataArea;
            //        //    field.AreaIndex = 3;
            //        //    field.Caption = EntityColumnResource.RatioToMPA;
            //        //    field.FieldName = DbColumns.RatioToMPA;
            //        //    field.CellFormat.FormatType = FormatType.Numeric;
            //        //    field.CellFormat.FormatString = "n2";
            //        //    field.SortMode = PivotSortMode.None;
            //        //    field.HeaderStyle.Font.Bold = true;
            //        //});
            //        //settings.Fields.Add(field =>
            //        //{
            //        //    field.Area = PivotArea.DataArea;
            //        //    field.AreaIndex = 3;
            //        //    field.Caption = EntityColumnResource.RatioToUsine;
            //        //    field.FieldName = DbColumns.RatioToUsine;
            //        //    field.CellFormat.FormatType = FormatType.Numeric;
            //        //    field.CellFormat.FormatString = "n2";
            //        //    field.SortMode = PivotSortMode.None;
            //        //    field.HeaderStyle.Font.Bold = true;
            //        //});
            //        break;
            //    default:
            //        break;
            //}


            return settings;
        }

     
        static Dictionary<PivotGridExportFormats, PivotGridExportType> exportTypes;
 
        public delegate ActionResult PivotGridExportMethod(PivotGridSettings settings, object dataObject);
        public class PivotGridExportType
        {
            public string Title { get; set; }
            public PivotGridExportMethod Method { get; set; }
        }
        

      
    }




}



