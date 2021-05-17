namespace PegasusPlus.Reports.Data
{
    partial class AitisiFreelance
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
            this.sqlDataFreelance = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.yearTextCaptionTextBox = new Telerik.Reporting.TextBox();
            this.dateStartCaptionTextBox = new Telerik.Reporting.TextBox();
            this.dateFinalCaptionTextBox = new Telerik.Reporting.TextBox();
            this.daysCaptionTextBox = new Telerik.Reporting.TextBox();
            this.incomeTextCaptionTextBox = new Telerik.Reporting.TextBox();
            this.moriaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.subjectCaptionTextBox = new Telerik.Reporting.TextBox();
            this.workEvidenceCaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.yearTextDataTextBox = new Telerik.Reporting.TextBox();
            this.dateStartDataTextBox = new Telerik.Reporting.TextBox();
            this.dateFinalDataTextBox = new Telerik.Reporting.TextBox();
            this.daysDataTextBox = new Telerik.Reporting.TextBox();
            this.incomeTextDataTextBox = new Telerik.Reporting.TextBox();
            this.moriaDataTextBox = new Telerik.Reporting.TextBox();
            this.subjectDataTextBox = new Telerik.Reporting.TextBox();
            this.workEvidenceDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataFreelance
            // 
            this.sqlDataFreelance.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlDataFreelance.Name = "sqlDataFreelance";
            this.sqlDataFreelance.SelectCommand = "SELECT        AitisiID, YearText, DateStart, DateFinal, Days, IncomeText, Moria, " +
    "Subject, WorkEvidence\r\nFROM            admDetailFreelance\r\nORDER BY YearText DES" +
    "C";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1999999284744263D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.yearTextCaptionTextBox,
            this.dateStartCaptionTextBox,
            this.dateFinalCaptionTextBox,
            this.daysCaptionTextBox,
            this.incomeTextCaptionTextBox,
            this.moriaCaptionTextBox,
            this.subjectCaptionTextBox,
            this.workEvidenceCaptionTextBox,
            this.textBox1});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // yearTextCaptionTextBox
            // 
            this.yearTextCaptionTextBox.CanGrow = true;
            this.yearTextCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10563389211893082D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.yearTextCaptionTextBox.Name = "yearTextCaptionTextBox";
            this.yearTextCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5943658351898193D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.yearTextCaptionTextBox.Style.Font.Bold = true;
            this.yearTextCaptionTextBox.Style.Font.Name = "Calibri";
            this.yearTextCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.yearTextCaptionTextBox.StyleName = "Caption";
            this.yearTextCaptionTextBox.Value = "етос еисодглатос";
            // 
            // dateStartCaptionTextBox
            // 
            this.dateStartCaptionTextBox.CanGrow = true;
            this.dateStartCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7001996040344238D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.dateStartCaptionTextBox.Name = "dateStartCaptionTextBox";
            this.dateStartCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7544264793395996D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
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
            this.dateFinalCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.4548263549804688D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.dateFinalCaptionTextBox.Name = "dateFinalCaptionTextBox";
            this.dateFinalCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7544271945953369D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
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
            this.daysCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.2094545364379883D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.daysCaptionTextBox.Name = "daysCaptionTextBox";
            this.daysCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2451713085174561D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.daysCaptionTextBox.Style.Font.Bold = true;
            this.daysCaptionTextBox.Style.Font.Name = "Calibri";
            this.daysCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.daysCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.daysCaptionTextBox.StyleName = "Caption";
            this.daysCaptionTextBox.Value = "глеяес";
            // 
            // incomeTextCaptionTextBox
            // 
            this.incomeTextCaptionTextBox.CanGrow = true;
            this.incomeTextCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.4548244476318359D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.incomeTextCaptionTextBox.Name = "incomeTextCaptionTextBox";
            this.incomeTextCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.1397395133972168D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.incomeTextCaptionTextBox.Style.Font.Bold = true;
            this.incomeTextCaptionTextBox.Style.Font.Name = "Calibri";
            this.incomeTextCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.incomeTextCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.incomeTextCaptionTextBox.StyleName = "Caption";
            this.incomeTextCaptionTextBox.Value = "еисодгла";
            // 
            // moriaCaptionTextBox
            // 
            this.moriaCaptionTextBox.CanGrow = true;
            this.moriaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.59476375579834D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.moriaCaptionTextBox.Name = "moriaCaptionTextBox";
            this.moriaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3598607778549194D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.moriaCaptionTextBox.Style.Font.Bold = true;
            this.moriaCaptionTextBox.Style.Font.Name = "Calibri";
            this.moriaCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.moriaCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.moriaCaptionTextBox.StyleName = "Caption";
            this.moriaCaptionTextBox.Value = "лояиа";
            // 
            // subjectCaptionTextBox
            // 
            this.subjectCaptionTextBox.CanGrow = true;
            this.subjectCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.95482349395752D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.subjectCaptionTextBox.Name = "subjectCaptionTextBox";
            this.subjectCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2451744079589844D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.subjectCaptionTextBox.Style.Font.Bold = true;
            this.subjectCaptionTextBox.Style.Font.Name = "Calibri";
            this.subjectCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.subjectCaptionTextBox.StyleName = "Caption";
            this.subjectCaptionTextBox.Value = "амтийеилемо";
            // 
            // workEvidenceCaptionTextBox
            // 
            this.workEvidenceCaptionTextBox.CanGrow = true;
            this.workEvidenceCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.200197219848633D), Telerik.Reporting.Drawing.Unit.Cm(0.69980001449584961D));
            this.workEvidenceCaptionTextBox.Name = "workEvidenceCaptionTextBox";
            this.workEvidenceCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3410520553588867D), Telerik.Reporting.Drawing.Unit.Cm(0.50019991397857666D));
            this.workEvidenceCaptionTextBox.Style.Font.Bold = true;
            this.workEvidenceCaptionTextBox.Style.Font.Name = "Calibri";
            this.workEvidenceCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.workEvidenceCaptionTextBox.StyleName = "Caption";
            this.workEvidenceCaptionTextBox.Value = "еццяажо";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50020003318786621D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.yearTextDataTextBox,
            this.dateStartDataTextBox,
            this.dateFinalDataTextBox,
            this.daysDataTextBox,
            this.incomeTextDataTextBox,
            this.moriaDataTextBox,
            this.subjectDataTextBox,
            this.workEvidenceDataTextBox});
            this.detail.Name = "detail";
            // 
            // yearTextDataTextBox
            // 
            this.yearTextDataTextBox.CanGrow = false;
            this.yearTextDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10563380271196365D), Telerik.Reporting.Drawing.Unit.Cm(0.00019984244136139751D));
            this.yearTextDataTextBox.Name = "yearTextDataTextBox";
            this.yearTextDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5943658351898193D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.yearTextDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.yearTextDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.yearTextDataTextBox.Style.Font.Name = "Calibri";
            this.yearTextDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.yearTextDataTextBox.StyleName = "Data";
            this.yearTextDataTextBox.Value = "= Fields.YearText";
            // 
            // dateStartDataTextBox
            // 
            this.dateStartDataTextBox.CanGrow = false;
            this.dateStartDataTextBox.Format = "{0:d}";
            this.dateStartDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7001996040344238D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.dateStartDataTextBox.Name = "dateStartDataTextBox";
            this.dateStartDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7544257640838623D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
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
            this.dateFinalDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.4548258781433105D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.dateFinalDataTextBox.Name = "dateFinalDataTextBox";
            this.dateFinalDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7544279098510742D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
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
            this.daysDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.2094545364379883D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.daysDataTextBox.Name = "daysDataTextBox";
            this.daysDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2451713085174561D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.daysDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.daysDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.daysDataTextBox.Style.Font.Name = "Calibri";
            this.daysDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.daysDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.daysDataTextBox.StyleName = "Data";
            this.daysDataTextBox.Value = "= Fields.Days";
            // 
            // incomeTextDataTextBox
            // 
            this.incomeTextDataTextBox.CanGrow = false;
            this.incomeTextDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.4548254013061523D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.incomeTextDataTextBox.Name = "incomeTextDataTextBox";
            this.incomeTextDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.1397383213043213D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.incomeTextDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.incomeTextDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.incomeTextDataTextBox.Style.Font.Name = "Calibri";
            this.incomeTextDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.incomeTextDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.incomeTextDataTextBox.StyleName = "Data";
            this.incomeTextDataTextBox.Value = "= Fields.IncomeText";
            // 
            // moriaDataTextBox
            // 
            this.moriaDataTextBox.CanGrow = false;
            this.moriaDataTextBox.Format = "{0:N2}";
            this.moriaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.59476375579834D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.moriaDataTextBox.Name = "moriaDataTextBox";
            this.moriaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3598607778549194D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.moriaDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.moriaDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.moriaDataTextBox.Style.Font.Name = "Calibri";
            this.moriaDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.moriaDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.moriaDataTextBox.StyleName = "Data";
            this.moriaDataTextBox.Value = "= Fields.Moria";
            // 
            // subjectDataTextBox
            // 
            this.subjectDataTextBox.CanGrow = false;
            this.subjectDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.95482349395752D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subjectDataTextBox.Name = "subjectDataTextBox";
            this.subjectDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2451744079589844D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.subjectDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.subjectDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.subjectDataTextBox.Style.Font.Name = "Calibri";
            this.subjectDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.subjectDataTextBox.StyleName = "Data";
            this.subjectDataTextBox.Value = "= Fields.Subject";
            // 
            // workEvidenceDataTextBox
            // 
            this.workEvidenceDataTextBox.CanGrow = false;
            this.workEvidenceDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.200197219848633D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.workEvidenceDataTextBox.Name = "workEvidenceDataTextBox";
            this.workEvidenceDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3410530090332031D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.workEvidenceDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.workEvidenceDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.workEvidenceDataTextBox.Style.Font.Name = "Calibri";
            this.workEvidenceDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.workEvidenceDataTextBox.StyleName = "Data";
            this.workEvidenceDataTextBox.Value = "= Fields.WorkEvidence";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10563389211893082D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3323955535888672D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "3) екеухеяо епаццекла";
            // 
            // AitisiFreelance
            // 
            this.DataSource = this.sqlDataFreelance;
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
            this.Name = "AitisiFreelance";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataFreelance;
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

        private Telerik.Reporting.SqlDataSource sqlDataFreelance;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox yearTextCaptionTextBox;
        private Telerik.Reporting.TextBox dateStartCaptionTextBox;
        private Telerik.Reporting.TextBox dateFinalCaptionTextBox;
        private Telerik.Reporting.TextBox daysCaptionTextBox;
        private Telerik.Reporting.TextBox incomeTextCaptionTextBox;
        private Telerik.Reporting.TextBox moriaCaptionTextBox;
        private Telerik.Reporting.TextBox subjectCaptionTextBox;
        private Telerik.Reporting.TextBox workEvidenceCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox yearTextDataTextBox;
        private Telerik.Reporting.TextBox dateStartDataTextBox;
        private Telerik.Reporting.TextBox dateFinalDataTextBox;
        private Telerik.Reporting.TextBox daysDataTextBox;
        private Telerik.Reporting.TextBox incomeTextDataTextBox;
        private Telerik.Reporting.TextBox moriaDataTextBox;
        private Telerik.Reporting.TextBox subjectDataTextBox;
        private Telerik.Reporting.TextBox workEvidenceDataTextBox;
        private Telerik.Reporting.TextBox textBox1;
    }
}