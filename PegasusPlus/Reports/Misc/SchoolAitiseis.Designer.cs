namespace PegasusPlus.Reports.Misc
{
    partial class SchoolAitiseis
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchoolAitiseis));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.protocolGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.protocolGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.schoolNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”CaptionTextBox = new Telerik.Reporting.TextBox();
            this.œ”œ”‘œCaptionTextBox = new Telerik.Reporting.TextBox();
            this.Û’ÕœÀœCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.schoolNameDataTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”DataTextBox = new Telerik.Reporting.TextBox();
            this.œ”œ”‘œDataTextBox = new Telerik.Reporting.TextBox();
            this.Û’ÕœÀœDataTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.sqlProkirixis = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = "SELECT        ProkirixiID, Protocol, SchoolID, SchoolName, –À«»œ”, ”’ÕœÀœ, –œ”œ”‘" +
    "œ\r\nFROM            statAitiseisSchoolsNumber\r\nORDER BY SchoolName";
            // 
            // protocolGroupHeaderSection
            // 
            this.protocolGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80000042915344238D);
            this.protocolGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
            this.protocolGroupHeaderSection.Name = "protocolGroupHeaderSection";
            // 
            // protocolGroupFooterSection
            // 
            this.protocolGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000060200691223D);
            this.protocolGroupFooterSection.Name = "protocolGroupFooterSection";
            this.protocolGroupFooterSection.Style.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "–—œ «—’Œ«:";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0531167984008789D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.73521614074707D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.Protocol";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.schoolNameCaptionTextBox,
            this.À«»œ”CaptionTextBox,
            this.œ”œ”‘œCaptionTextBox,
            this.Û’ÕœÀœCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.44863298535346985D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // schoolNameCaptionTextBox
            // 
            this.schoolNameCaptionTextBox.CanGrow = true;
            this.schoolNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.schoolNameCaptionTextBox.Name = "schoolNameCaptionTextBox";
            this.schoolNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.43541431427002D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.schoolNameCaptionTextBox.Style.Font.Bold = true;
            this.schoolNameCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.schoolNameCaptionTextBox.StyleName = "Caption";
            this.schoolNameCaptionTextBox.Value = "”◊œÀ… « ÃœÕ¡ƒ¡";
            // 
            // À«»œ”CaptionTextBox
            // 
            this.À«»œ”CaptionTextBox.CanGrow = true;
            this.À«»œ”CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.488531112670898D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.À«»œ”CaptionTextBox.Name = "À«»œ”CaptionTextBox";
            this.À«»œ”CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0997993946075439D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.À«»œ”CaptionTextBox.Style.Font.Bold = true;
            this.À«»œ”CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”CaptionTextBox.StyleName = "Caption";
            this.À«»œ”CaptionTextBox.Value = "–À«»œ”";
            // 
            // œ”œ”‘œCaptionTextBox
            // 
            this.œ”œ”‘œCaptionTextBox.CanGrow = true;
            this.œ”œ”‘œCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.588530540466309D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.œ”œ”‘œCaptionTextBox.Name = "œ”œ”‘œCaptionTextBox";
            this.œ”œ”‘œCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.œ”œ”‘œCaptionTextBox.Style.Font.Bold = true;
            this.œ”œ”‘œCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.œ”œ”‘œCaptionTextBox.StyleName = "Caption";
            this.œ”œ”‘œCaptionTextBox.Value = "–œ”œ”‘œ";
            // 
            // Û’ÕœÀœCaptionTextBox
            // 
            this.Û’ÕœÀœCaptionTextBox.CanGrow = true;
            this.Û’ÕœÀœCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.688530921936035D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Û’ÕœÀœCaptionTextBox.Name = "Û’ÕœÀœCaptionTextBox";
            this.Û’ÕœÀœCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.Û’ÕœÀœCaptionTextBox.Style.Font.Bold = true;
            this.Û’ÕœÀœCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Û’ÕœÀœCaptionTextBox.StyleName = "Caption";
            this.Û’ÕœÀœCaptionTextBox.Value = "”’ÕœÀœ";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2999999523162842D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.pictureBox1,
            this.textBox3});
            this.pageHeader.Name = "pageHeader";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60834169387817383D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox2,
            this.currentTimeTextBox,
            this.textBox6});
            this.pageFooter.Name = "pageFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.schoolNameDataTextBox,
            this.À«»œ”DataTextBox,
            this.œ”œ”‘œDataTextBox,
            this.Û’ÕœÀœDataTextBox});
            this.detail.Name = "detail";
            // 
            // schoolNameDataTextBox
            // 
            this.schoolNameDataTextBox.CanGrow = true;
            this.schoolNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.schoolNameDataTextBox.Name = "schoolNameDataTextBox";
            this.schoolNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.43541431427002D), Telerik.Reporting.Drawing.Unit.Cm(0.59999960660934448D));
            this.schoolNameDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.schoolNameDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.schoolNameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.schoolNameDataTextBox.StyleName = "Data";
            this.schoolNameDataTextBox.Value = "= Fields.SchoolName";
            // 
            // À«»œ”DataTextBox
            // 
            this.À«»œ”DataTextBox.CanGrow = true;
            this.À«»œ”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.488531112670898D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.À«»œ”DataTextBox.Name = "À«»œ”DataTextBox";
            this.À«»œ”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0997986793518066D), Telerik.Reporting.Drawing.Unit.Cm(0.59999960660934448D));
            this.À«»œ”DataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.À«»œ”DataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.À«»œ”DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”DataTextBox.StyleName = "Data";
            this.À«»œ”DataTextBox.Value = "= Fields.–À«»œ”";
            // 
            // œ”œ”‘œDataTextBox
            // 
            this.œ”œ”‘œDataTextBox.CanGrow = true;
            this.œ”œ”‘œDataTextBox.Format = "{0:P1}";
            this.œ”œ”‘œDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.588530540466309D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.œ”œ”‘œDataTextBox.Name = "œ”œ”‘œDataTextBox";
            this.œ”œ”‘œDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.59999960660934448D));
            this.œ”œ”‘œDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.œ”œ”‘œDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.œ”œ”‘œDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.œ”œ”‘œDataTextBox.StyleName = "Data";
            this.œ”œ”‘œDataTextBox.Value = "= Fields.–œ”œ”‘œ";
            // 
            // Û’ÕœÀœDataTextBox
            // 
            this.Û’ÕœÀœDataTextBox.CanGrow = true;
            this.Û’ÕœÀœDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.688530921936035D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Û’ÕœÀœDataTextBox.Name = "Û’ÕœÀœDataTextBox";
            this.Û’ÕœÀœDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.59999960660934448D));
            this.Û’ÕœÀœDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Û’ÕœÀœDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.Û’ÕœÀœDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Û’ÕœÀœDataTextBox.StyleName = "Data";
            this.Û’ÕœÀœDataTextBox.Value = "= Fields.”’ÕœÀœ";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.008341706357896328D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.70019990205764771D), Telerik.Reporting.Drawing.Unit.Cm(0.008341706357896328D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.7406325340271D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "≈÷¡—Ãœ√« PEGASUS+";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.4735422134399414D), Telerik.Reporting.Drawing.Unit.Cm(0.00844182912260294D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3147907257080078D), Telerik.Reporting.Drawing.Unit.Cm(0.59989988803863525D));
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.StyleName = "PageInfo";
            this.textBox6.Value = "= \"”ÂÎ.\" + PageNumber + \"/\" + PageCount";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.6022927761077881D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.186039924621582D), Telerik.Reporting.Drawing.Unit.Cm(0.59990030527114868D));
            this.textBox5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "ƒ…≈’»’Õ”« ¡—◊… «” ≈–¡√√≈ÀÃ¡‘… «” ≈ –¡…ƒ≈’”«” &  ¡‘¡—‘…”«”";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5491762161254883D), Telerik.Reporting.Drawing.Unit.Cm(1.2000004053115845D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.6022922992706299D), Telerik.Reporting.Drawing.Unit.Cm(0.6002001166343689D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.186039924621582D), Telerik.Reporting.Drawing.Unit.Cm(0.59990030527114868D));
            this.textBox3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "–À«»œ” ¡…‘«”≈ŸÕ ’–œÿ«÷…ŸÕ ≈ –¡…ƒ≈’‘… ŸÕ …≈ -œ¡≈ƒ";
            // 
            // sqlProkirixis
            // 
            this.sqlProkirixis.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlProkirixis.Name = "sqlProkirixis";
            this.sqlProkirixis.SelectCommand = "SELECT        ProkirixiID, Protocol\r\nFROM            Prokirixis\r\nORDER BY SchoolY" +
    "ear DESC";
            // 
            // SchoolAitiseis
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.ProkirixiID", Telerik.Reporting.FilterOperator.Equal, "= Parameters.prokirixiID.Value"));
            group1.GroupFooter = this.protocolGroupFooterSection;
            group1.GroupHeader = this.protocolGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Protocol"));
            group1.Name = "protocolGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.protocolGroupHeaderSection,
            this.protocolGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.detail});
            this.Name = "SchoolAitiseis";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlProkirixis;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.Protocol";
            reportParameter1.AvailableValues.ValueMember = "= Fields.ProkirixiID";
            reportParameter1.Name = "prokirixiID";
            reportParameter1.Text = "–ÒÔÍﬁÒıÓÁ";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.894166946411133D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection protocolGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.GroupFooterSection protocolGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox schoolNameCaptionTextBox;
        private Telerik.Reporting.TextBox À«»œ”CaptionTextBox;
        private Telerik.Reporting.TextBox œ”œ”‘œCaptionTextBox;
        private Telerik.Reporting.TextBox Û’ÕœÀœCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox schoolNameDataTextBox;
        private Telerik.Reporting.TextBox À«»œ”DataTextBox;
        private Telerik.Reporting.TextBox œ”œ”‘œDataTextBox;
        private Telerik.Reporting.TextBox Û’ÕœÀœDataTextBox;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.SqlDataSource sqlProkirixis;
    }
}