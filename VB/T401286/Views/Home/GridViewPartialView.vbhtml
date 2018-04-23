
@Code
    Dim grid As GridViewExtension = Html.DevExpress().GridView(
        Sub(settings)
            settings.Name = "Grid"
            settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartialView"}
            settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Home", .Action = "BatchUpdateRoomsPartial"}
            settings.SettingsEditing.Mode = GridViewEditingMode.Batch
            settings.SettingsPager.PageSize = 5
            settings.SettingsBehavior.AllowSort = False
            settings.ClientSideEvents.BatchEditStartEditing = "OnBatchEditStartEditing"
            settings.ClientSideEvents.BatchEditRowDeleting = "OnBatchEditRowDeleting"
            settings.ClientSideEvents.BatchEditRowInserting = "OnBatchEditRowInserting"
            settings.CommandColumn.Visible = True
            settings.CommandColumn.ShowDeleteButton = True
            settings.CommandColumn.ShowNewButtonInHeader = True
            settings.Width = 400
            settings.KeyFieldName = "ID"
            settings.Columns.Add("Number")
            settings.Columns.Add("Floor")
            settings.Columns.Add(
                    Sub(column)
                        column.FieldName = "IsReserved"
                        column.ColumnType = MVCxGridViewColumnType.CheckBox
                        column.EditorProperties().CheckBox(
                        Sub(p)
                            p.ClientSideEvents.CheckedChanged = "OnCellCheckedChanged"
                            p.ValidationSettings.Display = Display.Dynamic
                        End Sub)
                        column.SetHeaderTemplateContent(
                        Sub(c)
                            ViewContext.Writer.Write("<div style='text-align:center;'>")
                            Html.DevExpress().CheckBox(
                        Sub(headerCheckBoxSettings)
                            headerCheckBoxSettings.Name = "HeaderCheckBox"
                            headerCheckBoxSettings.Properties.AllowGrayed = True
                            headerCheckBoxSettings.Properties.AllowGrayedByClick = False
                            headerCheckBoxSettings.Properties.ClientSideEvents.CheckedChanged = "OnHeaderCheckBoxCheckedChanged"
                            headerCheckBoxSettings.Properties.ClientSideEvents.Init = "OnInitHeader"
                        End Sub).GetHtml()
                            ViewContext.Writer.Write("</div>")
                        End Sub)
                    End Sub)
        End Sub)
End Code
@grid.Bind(Model).GetHtml()