namespace PegasusPlus.Reports
{
    partial class A2Logo
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A2Logo));
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.textBox1,
            this.textBox2});
            this.detail.Name = "detail";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3999998569488525D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4999996423721314D), Telerik.Reporting.Drawing.Unit.Cm(1.0999001264572144D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.1001999378204346D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1998996734619141D), Telerik.Reporting.Drawing.Unit.Cm(0.89980012178421021D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "диеухумсг аявийгс епаццеклатийгс ейпаидеусгс йаи йатаятисгс";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(2.0002000331878662D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1998996734619141D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "тлгла диоийгсгс йаи ояцамысгс свокеиым";
            // 
            // A2Logo
            // 
            this.Culture = new System.Globalization.CultureInfo("el-GR");
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "DemoReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(8.1999998092651367D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
    }
}