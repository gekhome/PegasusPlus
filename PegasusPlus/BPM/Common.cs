using PegasusPlus.DAL;
using PegasusPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PegasusPlus.BPM
{
    public class Common
    {
        private PegasusPlusDBEntities db = new PegasusPlusDBEntities();

        #region String Functions (equivalent to VB)

        public static string Right(string text, int numberCharacters)
        {
            return text.Substring(numberCharacters > text.Length ? 0 : text.Length - numberCharacters);
        }

        public static string Left(string text, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length <= length)
                return text;
            else
                return text.Substring(0, length);
        }

        public static int Len(string text)
        {
            int _length;
            _length = text.Length;
            return _length;
        }

        public static byte Asc(string src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src + "")[0]);
        }

        public static char Chr(byte src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] { src })[0]);
        }

        public static bool isNumber(string param)
        {
            Regex isNum = new Regex("[^0-9]");
            return !isNum.IsMatch(param);
        }

        #endregion


        #region Date Functions
        /// <summary>
        /// Μετατρέπει τον αριθμό του μήνα σε λεκτικό
        /// στη γενική πτώση.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public string monthToGRstring(int m)
        {
            string stGRmonth = "";

            switch (m)
            {
                case 1: stGRmonth = "Ιανουαρίου"; break;
                case 2: stGRmonth = "Φεβρουαρίου"; break;
                case 3: stGRmonth = "Μαρτίου"; break;
                case 4: stGRmonth = "Απριλίου"; break;
                case 5: stGRmonth = "Μαϊου"; break;
                case 6: stGRmonth = "Ιουνίου"; break;
                case 7: stGRmonth = "Ιουλίου"; break;
                case 8: stGRmonth = "Αυγούστου"; break;
                case 9: stGRmonth = "Σεπτεμβρίου"; break;
                case 10: stGRmonth = "Οκτωβρίου"; break;
                case 11: stGRmonth = "Νοεμβρίου"; break;
                case 12: stGRmonth = "Δεκεμβρίου"; break;
                default: break;
            }
            return stGRmonth;
        }

        /// <summary>
        /// Ελέγχει αν η αρχική ημερομηνία είναι μικρότερη
        /// ή ίση με την τελική ημερομηνία.
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public bool ValidStartEndDates(DateTime dateStart, DateTime dateEnd)
        {
            bool result;

            if (dateStart > dateEnd)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες ανήκουν στο ίδιο έτος.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public bool DatesInSameYear(DateTime date1, DateTime date2)
        {
            bool result;

            if (date1.Year != date2.Year)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες είναι μέσα στο ίδιο Σχ. Έτος
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="schoolYearID"></param>
        /// <returns></returns>
        public bool DatesInSchoolYear(DateTime dateStart, DateTime dateEnd, int schoolYearID)
        {
            bool result = true;

            var schoolYear = (from s in db.SysSchoolYears
                              where s.SchoolYearID == schoolYearID
                              select new { s.DateStart, s.DateEnd }).FirstOrDefault();

            if (dateStart < schoolYear.DateStart || dateEnd > schoolYear.DateEnd)
                result = false;

            return result;
        }

        /// <summary>
        /// Ελέγχει αν το σχολικό έτος έχει τη μορφή ΝΝΝΝ-ΝΝΝΝ
        /// και αν τα έτη είναι συμβατά με τις ημερομηνίες
        /// έναρξης και λήξης.
        /// </summary>
        /// <param name="syear"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public bool VerifySchoolYear(string syear, DateTime d1, DateTime d2)
        {
            if (syear.IndexOf('-') == -1)
            {
                //ShowAdminMessage("Το σχολικό έτος πρέπει να είναι στη μορφή έτος1-έτος2.");
                return false;
            }

            string[] split = syear.Split(new Char[] { '-' });
            string sy1 = Convert.ToString(split[0]);
            string sy2 = Convert.ToString(split[1]);

            if (!isNumber(sy1) || !isNumber(sy2))
            {
                //ShowAdminMessage("Τα έτη δεν είναι αριθμοί.");
                return false;
            }
            else
            {
                int y1 = Convert.ToInt32(sy1);
                int y2 = Convert.ToInt32(sy2);

                if (y2 - y1 > 1 || y2 - y1 <= 0)
                {
                    //UserFunctions.ShowAdminMessage("Τα έτη δεν είναι σωστά.");
                    return false;
                }
                if (d1.Year != y1 || d2.Year != y2)
                {
                    //UserFunctions.ShowAdminMessage("Οι ημερομηνίες δεν συμφωνούν με τα έτη.");
                    return false;
                }
            }
            // at this point everything is ok
            return true;
        }

        /// <summary>
        /// Ελέγχει αν το χολικό έτος μορφής ΝΝΝΝ-ΝΝΝΝ υπάρχει ήδη.
        /// </summary>
        /// <param name="syear"></param>
        /// <returns></returns>
        public bool SchoolYearExists(int syear)
        {
            PegasusPlusDBEntities db = new PegasusPlusDBEntities();

            var syear_recs = (from s in db.SysSchoolYears
                              where s.SchoolYearID == syear
                              select s).Count();

            if (syear_recs != 0)
            {
                //ShowAdminMessage("Το σχολικό έτος υπάρχει ήδη.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Υπολογίζει τα έτη (στρογγυλοποιημένα) μεταξύ δύο ημερομηνιών
        /// </summary>
        /// <param name="sdate">αρχική ημερομηνία</param>
        /// <param name="edate">τελική ημερομηνία</param>
        /// <returns></returns>
        public int YearsDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            double _years = days / 365;

            int years = Convert.ToInt32(Math.Ceiling(_years));

            return years;
        }

        public int DaysDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            return days;
        }

        #endregion

        #region Protocol Generator

        //public string Get8Digits()
        //{
        //    var data = (from d in db.AITISIS orderby d.AITISI_ID descending select d).First();
        //    int newId = data.AITISI_ID + 1;

        //    return string.Format("{0:00000000}", newId);
        //}

        //public string GenerateProtocol()
        //{
        //    DateTime date1 = DateTime.Now;
        //    DateTime dateOnly = date1.Date;

        //    string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);               //Convert.ToString(dateOnly);

        //    string protocol = Get8Digits() + "/" + stDate;
        //    return protocol;
        //}

        //public string GenerateProtocol(DateTime date1)
        //{
        //    DateTime dateOnly = date1.Date;

        //    string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);               //Convert.ToString(dateOnly);

        //    string protocol = Get8Digits() + "/" + stDate;
        //    return protocol;
        //}

        #endregion

        public string GeneratePassword()
        {
            Random rnd = new Random();
            int random = rnd.Next(1000, 10000);
            return string.Format("{0:0000}", random);
        }

        #region Custom Pegasus Functions

        public float Max(params float[] values)
        {
            return Enumerable.Max(values);
        }
        public float Min(params float[] values)
        {
            return Enumerable.Min(values);
        }

        public string GetFinancialYear(int yearIndex)
        {
            var qry = (from d in db.SysTaxFree
                       where d.YearID == yearIndex
                       select new { d.YearText }).FirstOrDefault();

            if (qry == null) return "";
            else return qry.YearText;

        }

        public int GetOpenProkirixiID()
        {
            var qry = (from p in db.Prokirixis
                       where p.Status == 1
                       select new { p.ProkirixiID }).FirstOrDefault();

            if (qry == null)
                return 0;
            else
                return qry.ProkirixiID;
        }

        public int GetActiveProkirixiID()
        {
            var qry = (from p in db.Prokirixis
                       where p.Active == true
                       select new { p.ProkirixiID }).FirstOrDefault();

            if (qry == null)
                return 0;
            else
                return qry.ProkirixiID;
        }

        public int GetAdminProkirixiID()
        {
            var qry = (from p in db.Prokirixis
                       where p.Admin == true
                       select new { p.ProkirixiID }).FirstOrDefault();

            if (qry == null)
                return 0;
            else
                return qry.ProkirixiID;
        }

        public int GetEnstasiProkirixiID()
        {
            var qry = (from p in db.Prokirixis
                       where p.Enstaseis == true
                       select new { p.ProkirixiID }).FirstOrDefault();

            if (qry == null)
                return 0;
            else
                return qry.ProkirixiID;
        }

        public ProkirixisViewModel GetAdminProkirixi()
        {
            var data = (from d in db.Prokirixis
                        where d.Admin == true
                        select new ProkirixisViewModel
                        {
                            ProkirixiID = d.ProkirixiID,
                            Fek = d.Fek,
                            Protocol = d.Protocol,
                            SchoolYear = d.SchoolYear,
                            Dioikitis = d.Dioikitis,
                            DateStart = d.DateStart,
                            DateEnd = d.DateEnd
                        }).FirstOrDefault();
            return (data);
        }

        public ProkirixisViewModel GetActiveProkirixi()
        {
            var data = (from d in db.Prokirixis
                        where d.Active == true
                        select new ProkirixisViewModel
                        {
                            ProkirixiID = d.ProkirixiID,
                            Fek = d.Fek,
                            Protocol = d.Protocol,
                            SchoolYear = d.SchoolYear,
                            Dioikitis = d.Dioikitis,
                            DateStart = d.DateStart,
                            DateEnd = d.DateEnd
                        }).FirstOrDefault();
            return (data);
        }

        public string GetSchoolYearText(int syearId)
        {
            var data = (from d in db.SysSchoolYears
                        where d.SchoolYearID == syearId
                        select d).FirstOrDefault();

            string syearText = data.SchoolYearText;
            return (syearText);
        }

        public bool GetOpenProkirixiUserView()
        {
            var qry = (from p in db.Prokirixis
                       where p.UserView == true
                       select new { p.UserView }).FirstOrDefault();

            if (qry == null)
                return false;
            else
                return (bool)qry.UserView;
        }

        public bool GetOpenProkirixiEnstasi()
        {
            var qry = (from p in db.Prokirixis
                       where p.Enstaseis == true
                       select new { p.Enstaseis }).FirstOrDefault();

            if (qry == null)
                return false;
            else
                return (bool)qry.Enstaseis;
        }

        #endregion
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }

}