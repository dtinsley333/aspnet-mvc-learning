using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quality.Extensions
{
    //
   
    
    public class ConvertAS400Dates
    {
/// <summary>
/// This class converts the date values stored in ASI to short dates.  The conversion is tailored to the ASI dates.
/// Any year value greater than or equal 49 is for the century 2000.
/// And year value less than 49 is for the century 1900.
/// Century values are not returned from ASI. Leading zeros are not returned for years. 
/// Leading zeros are not shown for days of year unless the year value is greater than 0.
/// </summary>
/// <param name="sVal">string representing the values in ASI date fields.</param>
/// <returns></returns>
        public static DateTime GetDateValue(string sVal)
        {
            DateTime dtRet;
            try
            {
                DateTime thedate;
                if (sVal.Length < 4)
                {
                   
                    int dayofyear = Convert.ToInt16(sVal);
                     thedate = new DateTime(2000,1,1).AddDays(dayofyear-1);
                    return thedate;
                }
                if (sVal.Length >= 4)
                {
                //get ordinal day of year

                    string daysofyear=sVal.Substring(sVal.Length-3);
                    int dayofyear=Convert.ToInt16(daysofyear);
                    string yrname = sVal.Substring(0, sVal.Length - 3);
                  
                    //figure out which century to use
                    if (yrname.Length == 2)
                    {

                        if (Convert.ToInt16(yrname) >49)
                        {
                            yrname = "10" + yrname;
                        }
                        if (Convert.ToInt16(yrname) <= 49)
                        {
                            yrname = "20" + yrname;
                        }

                    
                    }
                    if (yrname.Length == 1)
                    {

                        if (Convert.ToInt16(yrname) >49)
                        {
                            yrname = "100" + yrname;
                        }
                        if (Convert.ToInt16(yrname) <= 49)
                        {
                            yrname = "200" + yrname;
                        }


                    }

                    int year=Convert.ToInt16(yrname);
                    thedate = new DateTime(year, 1, 1).AddDays(dayofyear - 1);
                    return thedate;
                }

            }
            catch (Exception)
            { }
            dtRet = new DateTime(1000, 01, 01, 0, 0, 0);
            return dtRet;
        }






    }
}