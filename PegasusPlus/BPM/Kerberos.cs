using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PegasusPlus.DAL;
using PegasusPlus.Models;
using PegasusPlus.BPM;

namespace PegasusPlus.BPM
{
    public class Kerberos
    {
        PegasusPlusDBEntities db = new PegasusPlusDBEntities();
        Common c = new Common();

        public const int TICKET_TIMEOUT_MINUTES = 240;

        private const int _WEEKS = 32;
        private const decimal WORKING_DAYS_YEAR = 365.0m;

        // ΑΝΑΦΟΡΑ: ΦΕΚ 3393/13.08.2020 Αρθρο 3 παρ. 6 (σελ.43766)
        // Ώρες που αντιστοιχούν σε 1 έτος - για αναγωγές
        // ΔΙΔΑΚΤΙΚΗ ΠΡΟΫΠΗΡΕΣΙΑ
        private const decimal _HOURSYEAR1 = 750;      // ΠΡΩΤΟΒΑΘΜΙΑ ΕΚΠΑΙΔΕΥΣΗ
        private const decimal _HOURSYEAR2 = 650;      // ΔΕΥΤΕΡΟΒΑΘΜΙΑ ΕΚΠΑΙΔΕΥΣΗ
        private const decimal _HOURSYEAR3 = 210;      // ΤΡΙΤΟΒΑΘΜΙΑ ΕΚΠΑΙΔΕΥΣΗ

        // ΔΙΔΑΚΤΙΚΗ ΕΜΠΕΙΡΙΑ
        // Μόρια ανά έτος για ΠΕ, ΤΕ, ΔΕ
        private const decimal _MORIA1 = 0.5m;       // ΠΡΩΤΟΒΑΘΜΙΑ
        private const decimal _MORIA2 = 1.0m;       // ΔΕΥΤΕΡΟΒΑΘΜΙΑ
        private const decimal _MORIA3 = 1.0m;       // ΤΡΙΤΟΒΑΘΜΙΑ

        private const decimal IEK_MORIA_MAX = 13;
        private const decimal OTHER_MORIA_MAX = 3;
        //double TEACH_MORIA_MAX = 18;

        // ΕΡΓΑΣΙΑΚΗ ΠΡΟΫΠΗΡΕΣΙΑ
        // Μόρια ανά έτος
        private const decimal WORK_MORIA_YEAR = 2.0m;
        // Γενικό μέγιστο όριο μορίων
        private const decimal WORK_MORIA_MAX = 43;

        // ΕΠΙΜΟΡΦΩΣΗ
        private const decimal EPIMORFOSI_MORIA_MAX = 2;
        // ΕΚΠΑΙΔΕΥΣΗ
        private const decimal EDUCATION_MORIA_MAX = 27;
        
        /// <summary>
        /// Υπολογίζει τις εργάσιμες ημέρες μεταξύ δύο ημερομηνιών,
        /// δηλ. χωρίς τα Σαββατοκύριακα.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="daycount"></returns>
        public int WorkingDays(DateTime initial_date, DateTime final_date)
        {
            int daycount = 0;

            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            while (date1 <= date2)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daycount++;
                        break;
                    default:
                        break;
                }
                date1 = date1.AddDays(1);
            }
            return daycount;
        }


        #region AFM VALIDATION

        /// ------------------------------------------------------------------------
        /// CheckAFM: Ελέγχει αν ένα ΑΦΜ είναι σωστό
        /// Το ΑΦΜ που θα ελέγξουμε
        /// true = ΑΦΜ σωστό, false = ΑΦΜ Λάθος
        /// Αυτή είναι η χρησιμοποιούμενη μεθοδος.
        /// Προσθήκη: Αποκλεισμός όταν όλα τα ψηφία = 0 (ο αλγόριθμος τα δέχεται!)
        /// Ημ/νια: 12/3/2013
        /// ------------------------------------------------------------------------
        public bool CheckAFM(string cAfm)
        {
            int nExp = 1;
            // Ελεγχος αν περιλαμβάνει μόνο γράμματα
            try { long nAfm = Convert.ToInt64(cAfm); }

            catch (Exception) { return false; }

            // Ελεγχος μήκους ΑΦΜ
            if (string.IsNullOrWhiteSpace(cAfm))
            {
                return false;
            }

            cAfm = cAfm.Trim();
            int nL = cAfm.Length;
            if (nL != 9) return false;

            // Έλεγχος αν όλα τα ψηφία είναι 0
            var count = cAfm.Count(x => x == '0');
            if (count == cAfm.Length) return false;

            //Υπολογισμός αν το ΑΦΜ είναι σωστό

            int nSum = 0;
            int xDigit = 0;
            int nT = 0;

            for (int i = nL - 2; i >= 0; i--)
            {
                xDigit = int.Parse(cAfm.Substring(i, 1));
                nT = xDigit * (int)(Math.Pow(2, nExp));
                nSum += nT;
                nExp++;
            }

            xDigit = int.Parse(cAfm.Substring(nL - 1, 1));

            nT = nSum / 11;
            int k = nT * 11;
            k = nSum - k;
            if (k == 10) k = 0;
            if (xDigit != k) return false;

            return true;

        }

        #endregion

        #region TEACHER AGE VALIDATION

        public bool ValidateBirthdate(Teachers teacher)
        {
            if (teacher.Birthdate == null) return false;

            DateTime _birthdate = (DateTime)teacher.Birthdate;

            if (!ValidBirthDate(_birthdate)) return false;
            else return true;
        }

        public bool ValidBirthDate(DateTime birthdate)
        {
            bool result = true;
            int maxAge = 75;
            int minAge = 18;

            DateTime minDate = DateTime.Today.Date.AddYears(-maxAge);
            DateTime maxDate = DateTime.Today.Date.AddYears(-minAge);

            if (birthdate >= minDate && birthdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }


        #endregion


        #region VALIDATION RULES

        public bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".PDF", ".JPG" };

            List<string> allowed_extensions = new List<string>(extensions);

            if (allowed_extensions.Contains(extension.ToUpper()))
                return true;
            return false;
        }

        public bool CanDeleteEpimorfosi(int epimorfosiId)
        {
            var data = (from d in db.EpimorfosiFiles where d.EpimorfosiID == epimorfosiId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        public bool CanDeleteAitisi(int aitisiId)
        {
            return false;
        }

        public bool CanDeleteTeaching(int expId)
        {
            var data = (from d in db.UploadsTeaching where d.ExperienceID == expId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        public bool CanDeleteVocation(int expId)
        {
            var data = (from d in db.UploadsVocation where d.ExperienceID == expId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        public bool CanDeleteFreelance(int expId)
        {
            var data = (from d in db.UploadsFreelance where d.ExperienceID == expId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        public bool CanDeleteEnstasi(int enstasiId)
        {
            var data = (from d in db.EnstaseisFiles where d.EnstasiID == enstasiId select d).Count();
            if (data > 0)
                return false;
            return true;
        }

        #endregion


        #region MORIA PARTIAL CALCULATIONS

        public decimal MoriaTeaching(WorkTeachingViewModel e)
        {
            decimal moria = 0.00m;
            decimal hours;
            decimal _moria = 0.0m;
            decimal _moriaMax = 0.0m;

            DateTime d1 = (DateTime)e.DateStart;
            DateTime d2 = (DateTime)e.DateFinal;

            decimal hw = e.HoursWeek ?? 0;
            decimal N = e.HoursTotal ?? 0;
            int domi = e.TeachType ?? 0;

            // user entered weekly hours which takes precedence            
            if (hw > 0)
            {
                decimal _weeks = WorkingDays(d1, d2) / 5.0m;
                N = (decimal)c.Min((float)(hw * _weeks), (float)(hw * _WEEKS));
            }
            switch (domi)
            {
                case 1:     // ΠΡΩΤΟΒΑΘΜΙΑ
                    {
                        hours = N / _HOURSYEAR1;
                        _moria = _MORIA1 * hours;
                        _moriaMax = _MORIA1;
                    }; break;
                case 2:     // ΔΕΥΤΕΡΟΒΑΘΜΙΑ
                    {
                        hours = N / _HOURSYEAR2;
                        _moria = _MORIA2 * hours;
                        _moriaMax = _MORIA2;
                    }; break;
                case 3:     // ΤΡΙΤΟΒΑΘΜΙΑ
                    {
                        hours = N / _HOURSYEAR3;
                        _moria = _MORIA3 * hours;
                        _moriaMax = _MORIA3;
                    }; break;
                case 4:     // ΙΕΚ, ΣΕΚ, ΠΣΕΚ
                    {
                        hours = N;
                        _moria = N / 200.0m;
                        _moriaMax = IEK_MORIA_MAX;
                    }; break;
                case 5:     // ΑΛΛΗ ΑΤΥΠΗ ΕΚΠΑΙΔΕΥΣΗ
                    {
                        hours = N;
                        _moria = N / 300.0m;
                        _moriaMax = OTHER_MORIA_MAX;
                    }; break;
                default: break;
            }
            moria = (decimal)c.Min((float)_moria, (float)_moriaMax);

            return moria;
        }

        public decimal MoriaVocation(WorkVocationViewModel e)
        {
            decimal moria = 0;

            decimal N = SetDaysAutoVocation(e);
            decimal days_manual = e.DaysManual ?? 0;

            // manual days override calculated value and max working days = 365
            if (days_manual > 0)
            {
                N = days_manual;
                decimal _moria = (N / WORKING_DAYS_YEAR) * WORK_MORIA_YEAR;
                moria = (decimal)c.Min((float)_moria, (float)WORK_MORIA_MAX);
            }
            else
            {
                decimal _moria = (N / WORKING_DAYS_YEAR) * WORK_MORIA_YEAR;
                moria = (decimal)c.Min((float)_moria, (float)WORK_MORIA_MAX);
            }

            return moria;
        }

        public decimal MoriaFreelance(WorkFreelanceViewModel e)
        {
            decimal moria = 0;

            int N = SetDaysAutoFreelance(e);
            int days_manual = e.DaysManual ?? 0;

            if (e.Income < e.IncomeTaxFree)
                return moria;

            // manual days override calculated value
            if (days_manual > 0)
            {
                N = days_manual;
            }
            decimal _moria = (N / WORKING_DAYS_YEAR) * WORK_MORIA_YEAR;
            moria = (decimal)c.Min((float)_moria, (float)WORK_MORIA_MAX);

            return moria;
        }

        public int SetDaysAutoVocation(WorkVocationViewModel e)
        {
            DateTime d1 = (DateTime)e.DateStart;
            DateTime d2 = (DateTime)e.DateFinal;

            int days = c.DaysDiff(d1, d2);
            return days;
        }

        public int SetDaysAutoFreelance(WorkFreelanceViewModel e)
        {
            DateTime d1 = (DateTime)e.DateStart;
            DateTime d2 = (DateTime)e.DateFinal;

            int days = c.DaysDiff(d1, d2);
            return days;
        }

        #endregion

        
        #region ΜΟΡΙΑ ΑΙΤΗΣΕΩΝ ΥΠΟΛΟΓΙΣΜΟΙ

        public decimal MoriaPtyxio(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;
            decimal moriaPtyxio1 = 0.0m;
            decimal moriaPtyxio2 = 0.0m;

            if (ap.OaedPtyxioConfirm == true)
            {
                if (ap.PtyxioType == 1)
                    moriaPtyxio1 = 7.0m;
                else if (ap.PtyxioType == 2)
                    moriaPtyxio1 = 5.0m;
                else if (ap.PtyxioType == 3 || ap.PtyxioType == 4)
                    moriaPtyxio1 = 3.0m;
            }

            if (ap.Ptyxio2Type > 0 && ap.OaedPtyxio2Confirm == true)
                moriaPtyxio2 = 1.0m;

            moria = moriaPtyxio1 + moriaPtyxio2;
            return moria;
        }

        public decimal MoriaMsc(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;

            if (ap.OaedMscConfirm == true)
            {
                if (ap.MscSynafeia == 1)
                    moria = 4.0m;
                else if (ap.MscSynafeia == 2 || ap.MscSynafeia == 3)
                    moria = 2.0m;
            }

            return moria;
        }

        public decimal MoriaPhd(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;

            if (ap.OaedPhdConfirm == true)
            {
                if (ap.PhdSynafeia == 1)
                    moria = 8.0m;
                else if(ap.PhdSynafeia == 2 || ap.PhdSynafeia == 3)
                    moria = 5.0m;
            }

            return moria;
        }

        public decimal MoriaLanguages(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;
            decimal moria1 = 0.0m;
            decimal moria2 = 0.0m;

            if (ap.OaedLanguage1Confirm == true)
            {
                if (ap.Language1Level == 1) moria1 = 3.0m;
                else if (ap.Language1Level == 2) moria1 = 2.0m;
                else if (ap.Language1Level == 3) moria1 = 1.0m;
            }
            if (ap.OaedLanguage2Confirm == true)
            {
                if (ap.Language2Level == 1) moria2 = 2.0m;
                else if (ap.Language2Level == 2) moria2 = 1.5m;
                else if (ap.Language2Level == 3) moria2 = 1.0m;
            }
            moria = moria1 + moria2;
            return moria;
        }

        public decimal MoriaComputer(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;

            if (ap.ComputerCertificate > 0 && ap.OaedComputerConfirm == true)
                moria = 3.0m;

            return moria;
        }
        
        public decimal MoriaCertifiedTrainer(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;

            if (ap.CertifiedTrainer == true && ap.OaedCertifiedConfirm == true)
                moria = 2.0m;

            return moria;
        }

        public decimal MoriaEpimorfosi(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;
            int N = 0;

            if (ap.EpimorfosiTotalHours >= 25 && ap.OaedEpimorfosiConfirm == true)
            {
                N = (int)ap.EpimorfosiTotalHours;
                moria = (N * 0.25m) / 100.0m;
            }
            moria = (decimal)c.Min((float)moria, (float)EPIMORFOSI_MORIA_MAX);

            return moria;
        }

        public decimal MoriaTeach(sqlAitisiProsonta ap)
        {
            decimal moria = (from d in db.qryFinalTeach where d.AitisiID == ap.AitisisID select d.MoriaTotal).FirstOrDefault() ?? 0.0m;
            return moria;
        }

        public decimal MoriaWork(sqlAitisiProsonta ap)
        {
            decimal moria = (from d in db.qryFinalWork where d.AitisiID == ap.AitisisID select d.MoriaTotal).FirstOrDefault() ?? 0.0m;
            return moria;
        }

        public decimal MoriaSumProsonta(sqlAitisiProsonta ap)
        {
            decimal moria = 0.0m;

            decimal m_ptyxio = MoriaPtyxio(ap);
            decimal m_msc = MoriaMsc(ap);
            decimal m_phd = MoriaPhd(ap);
            decimal m_lang = MoriaLanguages(ap);
            decimal m_computer = MoriaComputer(ap);
            decimal m_certified = MoriaCertifiedTrainer(ap);
            decimal m_epimorfosi = MoriaEpimorfosi(ap);
            decimal m_teach = MoriaTeach(ap);
            decimal m_work = MoriaWork(ap);

            decimal m_edu = m_ptyxio + m_msc + m_phd;
            m_edu = (decimal)c.Min((float)m_edu, (float)EDUCATION_MORIA_MAX);

            moria = m_edu + m_lang + m_computer + m_certified + m_epimorfosi + m_teach + m_work;
            return moria;
        }

        public decimal MoriaAnergia(sqlAitisiProsonta ap)
        {
            decimal moria_anergia = 0.0m;
            decimal moria_base = 0.02m;

            int index_anergia = ap.SocialAnergos ?? 0;
            if (index_anergia == 0)
            {
                return moria_anergia;
            }
            if (index_anergia == 1)
            {
                moria_anergia = moria_base * MoriaSumProsonta(ap);
                return moria_anergia;
            }
            else
            {
                moria_anergia = (moria_base + (index_anergia - 1) * moria_base) * MoriaSumProsonta(ap);
                return moria_anergia;
            }
        }

        public decimal MoriaSocial(sqlAitisiProsonta ap)
        {
            decimal moriaSocial = 0.0m;
            decimal p = 0.10m;

            if (ap.SocialAmea == true)
                moriaSocial += p * MoriaSumProsonta(ap);
            if (ap.SocialPolyteknos == true)
                moriaSocial += p * MoriaSumProsonta(ap);
            if (ap.SocialSingleParent == true)
                moriaSocial += p * MoriaSumProsonta(ap);
            if (ap.SocialTriteknos == true)
                moriaSocial += p * MoriaSumProsonta(ap);

            return moriaSocial;
        }

        public decimal MoriaTotal(sqlAitisiProsonta ap)
        {
            decimal moriaSum = 0.0m;

            moriaSum = MoriaSumProsonta(ap) + MoriaSocial(ap) + MoriaAnergia(ap);
            return moriaSum;
        }

        #endregion
    }
}