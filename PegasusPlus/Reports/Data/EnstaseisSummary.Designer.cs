namespace PegasusPlus.Reports.Data
{
    partial class EnstaseisSummary
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnstaseisSummary));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.protocolGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.protocolGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.periferiakiNameGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.periferiakiNameGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.schoolNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”CaptionTextBox = new Telerik.Reporting.TextBox();
            this.Û’ÕœÀœCaptionTextBox = new Telerik.Reporting.TextBox();
            this.œ”œ”‘œCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.schoolNameDataTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”DataTextBox = new Telerik.Reporting.TextBox();
            this.Û’ÕœÀœDataTextBox = new Telerik.Reporting.TextBox();
            this.œ”œ”‘œDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.sqlPeriferiakes = new Telerik.Reporting.SqlDataSource();
            this.sqlProkirixis = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = resources.GetString("sqlDataSource.SelectCommand");
            // 
            // protocolGroupHeaderSection
            // 
            this.protocolGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.90000033378601074D);
            this.protocolGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox1});
            this.protocolGroupHeaderSection.Name = "protocolGroupHeaderSection";
            // 
            // protocolGroupFooterSection
            // 
            this.protocolGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80522811412811279D);
            this.protocolGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13});
            this.protocolGroupFooterSection.Name = "protocolGroupFooterSection";
            // 
            // periferiakiNameGroupHeaderSection
            // 
            this.periferiakiNameGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80010014772415161D);
            this.periferiakiNameGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4});
            this.periferiakiNameGroupHeaderSection.Name = "periferiakiNameGroupHeaderSection";
            // 
            // periferiakiNameGroupFooterSection
            // 
            this.periferiakiNameGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0572997331619263D);
            this.periferiakiNameGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox7,
            this.textBox8,
            this.textBox9});
            this.periferiakiNameGroupFooterSection.Name = "periferiakiNameGroupFooterSection";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.À«»œ”CaptionTextBox,
            this.Û’ÕœÀœCaptionTextBox,
            this.œ”œ”‘œCaptionTextBox,
            this.schoolNameCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.54219961166381836D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // schoolNameCaptionTextBox
            // 
            this.schoolNameCaptionTextBox.CanGrow = true;
            this.schoolNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.schoolNameCaptionTextBox.Name = "schoolNameCaptionTextBox";
            this.schoolNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.447083473205566D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.schoolNameCaptionTextBox.Style.Font.Bold = true;
            this.schoolNameCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.schoolNameCaptionTextBox.StyleName = "Caption";
            this.schoolNameCaptionTextBox.Value = "≈ –¡…ƒ≈’‘… « ÃœÕ¡ƒ¡";
            // 
            // À«»œ”CaptionTextBox
            // 
            this.À«»œ”CaptionTextBox.CanGrow = true;
            this.À«»œ”CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.À«»œ”CaptionTextBox.Name = "À«»œ”CaptionTextBox";
            this.À«»œ”CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.À«»œ”CaptionTextBox.Style.Font.Bold = true;
            this.À«»œ”CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”CaptionTextBox.StyleName = "Caption";
            this.À«»œ”CaptionTextBox.Value = "–À«»œ”";
            // 
            // Û’ÕœÀœCaptionTextBox
            // 
            this.Û’ÕœÀœCaptionTextBox.CanGrow = true;
            this.Û’ÕœÀœCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.600201606750488D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Û’ÕœÀœCaptionTextBox.Name = "Û’ÕœÀœCaptionTextBox";
            this.Û’ÕœÀœCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8997995853424072D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.Û’ÕœÀœCaptionTextBox.Style.Font.Bold = true;
            this.Û’ÕœÀœCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Û’ÕœÀœCaptionTextBox.StyleName = "Caption";
            this.Û’ÕœÀœCaptionTextBox.Value = "”’ÕœÀœ";
            // 
            // œ”œ”‘œCaptionTextBox
            // 
            this.œ”œ”‘œCaptionTextBox.CanGrow = true;
            this.œ”œ”‘œCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.500201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.œ”œ”‘œCaptionTextBox.Name = "œ”œ”‘œCaptionTextBox";
            this.œ”œ”‘œCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3410484790802D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.œ”œ”‘œCaptionTextBox.Style.Font.Bold = true;
            this.œ”œ”‘œCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.œ”œ”‘œCaptionTextBox.StyleName = "Caption";
            this.œ”œ”‘œCaptionTextBox.Value = "–œ”œ”‘œ";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2999997138977051D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.pictureBox1,
            this.textBox5});
            this.pageHeader.Name = "pageHeader";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60010010004043579D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.currentTimeTextBox,
            this.pictureBox2});
            this.pageFooter.Name = "pageFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.schoolNameDataTextBox,
            this.À«»œ”DataTextBox,
            this.Û’ÕœÀœDataTextBox,
            this.œ”œ”‘œDataTextBox});
            this.detail.Name = "detail";
            // 
            // schoolNameDataTextBox
            // 
            this.schoolNameDataTextBox.CanGrow = true;
            this.schoolNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.schoolNameDataTextBox.Name = "schoolNameDataTextBox";
            this.schoolNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.447083473205566D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.schoolNameDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.schoolNameDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.schoolNameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.schoolNameDataTextBox.StyleName = "Data";
            this.schoolNameDataTextBox.Value = "= Fields.SchoolName";
            // 
            // À«»œ”DataTextBox
            // 
            this.À«»œ”DataTextBox.CanGrow = true;
            this.À«»œ”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.À«»œ”DataTextBox.Name = "À«»œ”DataTextBox";
            this.À«»œ”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998027324676514D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.À«»œ”DataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.À«»œ”DataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.À«»œ”DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”DataTextBox.StyleName = "Data";
            this.À«»œ”DataTextBox.Value = "= Fields.–À«»œ”";
            // 
            // Û’ÕœÀœDataTextBox
            // 
            this.Û’ÕœÀœDataTextBox.CanGrow = true;
            this.Û’ÕœÀœDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.600202560424805D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Û’ÕœÀœDataTextBox.Name = "Û’ÕœÀœDataTextBox";
            this.Û’ÕœÀœDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8997979164123535D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.Û’ÕœÀœDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Û’ÕœÀœDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.Û’ÕœÀœDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Û’ÕœÀœDataTextBox.StyleName = "Data";
            this.Û’ÕœÀœDataTextBox.Value = "= Fields.”’ÕœÀœ";
            // 
            // œ”œ”‘œDataTextBox
            // 
            this.œ”œ”‘œDataTextBox.CanGrow = true;
            this.œ”œ”‘œDataTextBox.Format = "{0:P1}";
            this.œ”œ”‘œDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.500201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.œ”œ”‘œDataTextBox.Name = "œ”œ”‘œDataTextBox";
            this.œ”œ”‘œDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3410484790802D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.œ”œ”‘œDataTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.œ”œ”‘œDataTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.œ”œ”‘œDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.œ”œ”‘œDataTextBox.StyleName = "Data";
            this.œ”œ”‘œDataTextBox.Value = "= Fields.–œ”œ”‘œ";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.4208250045776367D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.47324275970459D), Telerik.Reporting.Drawing.Unit.Cm(0.59989988803863525D));
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.StyleName = "PageInfo";
            this.textBox15.Value = "= \"”ÂÎ.\" + PageNumber + \"/\" + PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.67999237775802612D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.7406325340271D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "≈÷¡—Ãœ√« PEGASUS+";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5493756532669067D), Telerik.Reporting.Drawing.Unit.Cm(0.6002001166343689D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.277080535888672D), Telerik.Reporting.Drawing.Unit.Cm(0.59990030527114868D));
            this.textBox6.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = "”’√ ≈Õ‘—Ÿ‘… «  ¡‘¡”‘¡”« –À«»œ’” ≈Õ”‘¡”≈ŸÕ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5491762161254883D), Telerik.Reporting.Drawing.Unit.Cm(1.2000004053115845D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5493761301040649D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.277080535888672D), Telerik.Reporting.Drawing.Unit.Cm(0.59990030527114868D));
            this.textBox5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "ƒ…≈’»’Õ”« ¡—◊… «” ≈–¡√√≈ÀÃ¡‘… «” ≈ –¡…ƒ≈’”«” &  ¡‘¡—‘…”«”";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8471839427948D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.97927188873291D), Telerik.Reporting.Drawing.Unit.Cm(0.69990003108978271D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.Protocol";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8470833301544189D), Telerik.Reporting.Drawing.Unit.Cm(0.69990003108978271D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "–—œ «—’Œ«:";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.826456069946289D), Telerik.Reporting.Drawing.Unit.Cm(0.7999998927116394D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "= Fields.PeriferiakiName";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.2468433678150177D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.5D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "= Fields.PeriferiakiShortName";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Format = "{0:P1}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.500201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0.2468433678150177D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3262543678283691D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.StyleName = "Data";
            this.textBox7.Value = "=(Sum(Fields.–À«»œ”)*1.0)/Sum(Fields.”’ÕœÀœ)";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.600201606750488D), Telerik.Reporting.Drawing.Unit.Cm(0.2468433678150177D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8997979164123535D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.StyleName = "Data";
            this.textBox8.Value = "= Sum(Fields.”’ÕœÀœ)";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0.2468433678150177D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998027324676514D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "= Sum(Fields.–À«»œ”)";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0.20522810518741608D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998027324676514D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox10.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "= Sum(Fields.–À«»œ”)";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.600202560424805D), Telerik.Reporting.Drawing.Unit.Cm(0.20522810518741608D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8997979164123535D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox11.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.StyleName = "Data";
            this.textBox11.Value = "= Fields.–À«»œ”_¡…‘«”≈…”";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = true;
            this.textBox12.Format = "{0:P1}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.500201225280762D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3938665390014648D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox12.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.StyleName = "Data";
            this.textBox12.Value = "=(Sum(Fields.–À«»œ”)*1.0)/ Fields.–À«»œ”_¡…‘«”≈…”";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.051043394953012466D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.448956489562988D), Telerik.Reporting.Drawing.Unit.Cm(0.589544415473938D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox13.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox13.StyleName = "Caption";
            this.textBox13.Value = "= \"”’Õœÿ« √…¡ ‘«Õ –—œ «—’Œ«: \" + Fields.Protocol";
            // 
            // sqlPeriferiakes
            // 
            this.sqlPeriferiakes.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlPeriferiakes.Name = "sqlPeriferiakes";
            this.sqlPeriferiakes.SelectCommand = "SELECT        PeriferiakiID, PeriferiakiShortName\r\nFROM            SysPeriferiake" +
    "s";
            // 
            // sqlProkirixis
            // 
            this.sqlProkirixis.ConnectionString = "PegasusPlusDBEntities1";
            this.sqlProkirixis.Name = "sqlProkirixis";
            this.sqlProkirixis.SelectCommand = "SELECT        ProkirixiID, Protocol\r\nFROM            Prokirixis\r\nORDER BY SchoolY" +
    "ear DESC";
            // 
            // EnstaseisSummary
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.ProkirixiID", Telerik.Reporting.FilterOperator.Equal, "= Parameters.prokirixiID.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.PeriferiakiID", Telerik.Reporting.FilterOperator.In, "= Parameters.periferiakiID.Value"));
            group1.GroupFooter = this.protocolGroupFooterSection;
            group1.GroupHeader = this.protocolGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Protocol"));
            group1.Name = "protocolGroup";
            group2.GroupFooter = this.periferiakiNameGroupFooterSection;
            group2.GroupHeader = this.periferiakiNameGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.PeriferiakiName"));
            group2.Name = "periferiakiNameGroup";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.protocolGroupHeaderSection,
            this.protocolGroupFooterSection,
            this.periferiakiNameGroupHeaderSection,
            this.periferiakiNameGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.detail});
            this.Name = "EnstaseisSummary";
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
            reportParameter2.AllowNull = true;
            reportParameter2.AutoRefresh = true;
            reportParameter2.AvailableValues.DataSource = this.sqlPeriferiakes;
            reportParameter2.AvailableValues.DisplayMember = "= Fields.PeriferiakiShortName";
            reportParameter2.AvailableValues.ValueMember = "= Fields.PeriferiakiID";
            reportParameter2.MultiValue = true;
            reportParameter2.Name = "periferiakiID";
            reportParameter2.Text = "–ÂÒÈˆÂÒÂÈ·Íﬁ";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
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
        private Telerik.Reporting.GroupFooterSection protocolGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection periferiakiNameGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection periferiakiNameGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox schoolNameCaptionTextBox;
        private Telerik.Reporting.TextBox À«»œ”CaptionTextBox;
        private Telerik.Reporting.TextBox Û’ÕœÀœCaptionTextBox;
        private Telerik.Reporting.TextBox œ”œ”‘œCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox schoolNameDataTextBox;
        private Telerik.Reporting.TextBox À«»œ”DataTextBox;
        private Telerik.Reporting.TextBox Û’ÕœÀœDataTextBox;
        private Telerik.Reporting.TextBox œ”œ”‘œDataTextBox;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.SqlDataSource sqlPeriferiakes;
        private Telerik.Reporting.SqlDataSource sqlProkirixis;
    }
}