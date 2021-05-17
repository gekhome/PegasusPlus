namespace PegasusPlus.Reports.Data
{
    partial class AitisiVocation
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataVocation = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.dateStartCaptionTextBox = new Telerik.Reporting.TextBox();
            this.dateFinalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.daysCaptionTextBox = new Telerik.Reporting.TextBox();
            this.moriaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.documentForeasCaptionTextBox = new Telerik.Reporting.TextBox();
            this.subjectCaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.dateStartDataTextBox = new Telerik.Reporting.TextBox();
            this.dateFinalDataTextBox = new Telerik.Reporting.TextBox();
            this.daysDataTextBox = new Telerik.Reporting.TextBox();
            this.moriaDataTextBox = new Telerik.Reporting.TextBox();
            this.documentForeasDataTextBox = new Telerik.Reporting.TextBox();
            this.subjectDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataVocation
            // 
            this.sqlDataVocation.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlDataVocation.Name = "sqlDataVocation";
            this.sqlDataVocation.SelectCommand = "SELECT        AitisiID, DateStart, DateFinal, Days, Moria, Subject, DocumentForea" +
    "s, Position\r\nFROM            admDetailVocation\r\nORDER BY DateStart DESC";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1999999284744263D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.dateStartCaptionTextBox,
            this.dateFinalCaptionTextBox,
            this.daysCaptionTextBox,
            this.moriaCaptionTextBox,
            this.documentForeasCaptionTextBox,
            this.subjectCaptionTextBox,
            this.textBox1,
            this.textBox2});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39980009198188782D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // dateStartCaptionTextBox
            // 
            this.dateStartCaptionTextBox.CanGrow = true;
            this.dateStartCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916780114173889D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.dateStartCaptionTextBox.Name = "dateStartCaptionTextBox";
            this.dateStartCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5470831394195557D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.dateStartCaptionTextBox.Style.Font.Bold = true;
            this.dateStartCaptionTextBox.Style.Font.Name = "Calibri";
            this.dateStartCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.dateStartCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.dateStartCaptionTextBox.StyleName = "Caption";
            this.dateStartCaptionTextBox.Value = "апо";
            // 
            // dateFinalCaptionTextBox
            // 
            this.dateFinalCaptionTextBox.CanGrow = true;
            this.dateFinalCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6001996994018555D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.dateFinalCaptionTextBox.Name = "dateFinalCaptionTextBox";
            this.dateFinalCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5470836162567139D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.dateFinalCaptionTextBox.Style.Font.Bold = true;
            this.dateFinalCaptionTextBox.Style.Font.Name = "Calibri";
            this.dateFinalCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.dateFinalCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.dateFinalCaptionTextBox.StyleName = "Caption";
            this.dateFinalCaptionTextBox.Value = "еыс";
            // 
            // daysCaptionTextBox
            // 
            this.daysCaptionTextBox.CanGrow = true;
            this.daysCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.1474838256835938D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.daysCaptionTextBox.Name = "daysCaptionTextBox";
            this.daysCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3997994661331177D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.daysCaptionTextBox.Style.Font.Bold = true;
            this.daysCaptionTextBox.Style.Font.Name = "Calibri";
            this.daysCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.daysCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.daysCaptionTextBox.StyleName = "Caption";
            this.daysCaptionTextBox.Value = "глеяес";
            // 
            // moriaCaptionTextBox
            // 
            this.moriaCaptionTextBox.CanGrow = true;
            this.moriaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5474834442138672D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.moriaCaptionTextBox.Name = "moriaCaptionTextBox";
            this.moriaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3998008966445923D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.moriaCaptionTextBox.Style.Font.Bold = true;
            this.moriaCaptionTextBox.Style.Font.Name = "Calibri";
            this.moriaCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.moriaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.moriaCaptionTextBox.StyleName = "Caption";
            this.moriaCaptionTextBox.Value = "лояиа";
            // 
            // documentForeasCaptionTextBox
            // 
            this.documentForeasCaptionTextBox.CanGrow = true;
            this.documentForeasCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9474849700927734D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.documentForeasCaptionTextBox.Name = "documentForeasCaptionTextBox";
            this.documentForeasCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.25251579284668D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.documentForeasCaptionTextBox.Style.Font.Bold = true;
            this.documentForeasCaptionTextBox.Style.Font.Name = "Calibri";
            this.documentForeasCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.documentForeasCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.documentForeasCaptionTextBox.StyleName = "Caption";
            this.documentForeasCaptionTextBox.Value = "жояеас-еяцодотгс";
            // 
            // subjectCaptionTextBox
            // 
            this.subjectCaptionTextBox.CanGrow = true;
            this.subjectCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19D), Telerik.Reporting.Drawing.Unit.Cm(0.63279122114181519D));
            this.subjectCaptionTextBox.Name = "subjectCaptionTextBox";
            this.subjectCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.5412497520446777D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.subjectCaptionTextBox.Style.Font.Bold = true;
            this.subjectCaptionTextBox.Style.Font.Name = "Calibri";
            this.subjectCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.subjectCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.subjectCaptionTextBox.StyleName = "Caption";
            this.subjectCaptionTextBox.Value = "амтийеилемо хесгс";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.dateStartDataTextBox,
            this.dateFinalDataTextBox,
            this.daysDataTextBox,
            this.moriaDataTextBox,
            this.documentForeasDataTextBox,
            this.subjectDataTextBox,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // dateStartDataTextBox
            // 
            this.dateStartDataTextBox.CanGrow = false;
            this.dateStartDataTextBox.Format = "{0:d}";
            this.dateStartDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916780114173889D), Telerik.Reporting.Drawing.Unit.Cm(0.00019984244136139751D));
            this.dateStartDataTextBox.Name = "dateStartDataTextBox";
            this.dateStartDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5470831394195557D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.dateStartDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.dateStartDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.dateStartDataTextBox.Style.Font.Name = "Calibri";
            this.dateStartDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.dateStartDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.dateStartDataTextBox.StyleName = "Data";
            this.dateStartDataTextBox.Value = "= Fields.DateStart";
            // 
            // dateFinalDataTextBox
            // 
            this.dateFinalDataTextBox.CanGrow = false;
            this.dateFinalDataTextBox.Format = "{0:d}";
            this.dateFinalDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6002001762390137D), Telerik.Reporting.Drawing.Unit.Cm(0.00019984244136139751D));
            this.dateFinalDataTextBox.Name = "dateFinalDataTextBox";
            this.dateFinalDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5470831394195557D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.dateFinalDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.dateFinalDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.dateFinalDataTextBox.Style.Font.Name = "Calibri";
            this.dateFinalDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.dateFinalDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.dateFinalDataTextBox.StyleName = "Data";
            this.dateFinalDataTextBox.Value = "= Fields.DateFinal";
            // 
            // daysDataTextBox
            // 
            this.daysDataTextBox.CanGrow = false;
            this.daysDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.1474838256835938D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.daysDataTextBox.Name = "daysDataTextBox";
            this.daysDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3997994661331177D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.daysDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.daysDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.daysDataTextBox.Style.Font.Name = "Calibri";
            this.daysDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.daysDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.daysDataTextBox.StyleName = "Data";
            this.daysDataTextBox.Value = "= Fields.Days";
            // 
            // moriaDataTextBox
            // 
            this.moriaDataTextBox.CanGrow = false;
            this.moriaDataTextBox.Format = "{0:N2}";
            this.moriaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5474834442138672D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.moriaDataTextBox.Name = "moriaDataTextBox";
            this.moriaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3998008966445923D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.moriaDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.moriaDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.moriaDataTextBox.Style.Font.Name = "Calibri";
            this.moriaDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.moriaDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.moriaDataTextBox.StyleName = "Data";
            this.moriaDataTextBox.Value = "= Fields.Moria";
            // 
            // documentForeasDataTextBox
            // 
            this.documentForeasDataTextBox.CanGrow = false;
            this.documentForeasDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9474849700927734D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.documentForeasDataTextBox.Name = "documentForeasDataTextBox";
            this.documentForeasDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.25251579284668D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.documentForeasDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.documentForeasDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.documentForeasDataTextBox.Style.Font.Name = "Calibri";
            this.documentForeasDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.documentForeasDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.documentForeasDataTextBox.StyleName = "Data";
            this.documentForeasDataTextBox.Value = "= Fields.DocumentForeas";
            // 
            // subjectDataTextBox
            // 
            this.subjectDataTextBox.CanGrow = false;
            this.subjectDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subjectDataTextBox.Name = "subjectDataTextBox";
            this.subjectDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.5412507057189941D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.subjectDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.subjectDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.subjectDataTextBox.Style.Font.Name = "Calibri";
            this.subjectDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.subjectDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.subjectDataTextBox.StyleName = "Data";
            this.subjectDataTextBox.Value = "= Fields.Subject";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3323955535888672D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "2) епаццеклатийес пяошпгяесиес";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.200199127197266D), Telerik.Reporting.Drawing.Unit.Cm(0.63269138336181641D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7996020317077637D), Telerik.Reporting.Drawing.Unit.Cm(0.56720870733261108D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "хесг";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = false;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.200198173522949D), Telerik.Reporting.Drawing.Unit.Cm(0.00020001066150143743D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7996006011962891D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox3.Style.Font.Name = "Calibri";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "= Fields.Position";
            // 
            // AitisiVocation
            // 
            this.DataSource = this.sqlDataVocation;
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.AitisiID", Telerik.Reporting.FilterOperator.Equal, "= Parameters.aitisiID.Value"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.detail});
            this.Name = "AitisiVocation";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataVocation;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.AitisiID";
            reportParameter1.AvailableValues.ValueMember = "= Fields.AitisiID";
            reportParameter1.Name = "aitisiID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Bold = true;
            styleRule2.Style.Font.Italic = false;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Style.Font.Strikeout = false;
            styleRule2.Style.Font.Underline = false;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule3.Style.Color = System.Drawing.Color.Black;
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(25.594165802001953D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataVocation;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox dateStartCaptionTextBox;
        private Telerik.Reporting.TextBox dateFinalCaptionTextBox;
        private Telerik.Reporting.TextBox daysCaptionTextBox;
        private Telerik.Reporting.TextBox moriaCaptionTextBox;
        private Telerik.Reporting.TextBox documentForeasCaptionTextBox;
        private Telerik.Reporting.TextBox subjectCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox dateStartDataTextBox;
        private Telerik.Reporting.TextBox dateFinalDataTextBox;
        private Telerik.Reporting.TextBox daysDataTextBox;
        private Telerik.Reporting.TextBox moriaDataTextBox;
        private Telerik.Reporting.TextBox documentForeasDataTextBox;
        private Telerik.Reporting.TextBox subjectDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
    }
}