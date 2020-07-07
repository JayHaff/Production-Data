using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kv_repair
{
    public class CAT
    {
        public string catNum { get; set; }
        public string geNum { get; set; }
        public DateTime matchPoint { get; set; }
        
        public int typeNum;
        
        public List<Pack1> result;
        List<Pack1> resultge;
        List<QueryInput2> result2;
        List<double> times;
        

        public CAT(string catNum, string geNum, DateTime matchPoint)
        {
            this.catNum = catNum;
            this.geNum = geNum;
            this.matchPoint = matchPoint;
            resultge = new List<Pack1>();
            times = new List<double> ();
            result = DatabaseHelper.PackResult(catNum);
            //result2 = DatabaseHelper.MatchPointData2(catNum);



        }



        public string TypeofAMR()
        {
            string type = "";
            if (catNum.ElementAt(5) == '0')
            {
                type = "None";
            }
            if (catNum.ElementAt(5) == '1')
            {
                type = "200B Telephone Modem";
            }

            if (catNum.ElementAt(5) == '2')
            {
                type = "RSX Board (RS-232)";
            }
            if (catNum.ElementAt(5) == '3')
            {
                type = "53ESS-ERT-ITRON";
            }
            if (catNum.ElementAt(5) == '4')
            {
                type = "CELLNET/L&G";
            }
            if (catNum.ElementAt(5) == '5')
            {
                type = "RS-485/Modbus";
            }
         
            if (catNum.ElementAt(5) == '8')
            {
                type = "SSN";
            }
            
            if (catNum.ElementAt(5) == '9')
            {
                type = "UMT";
            }
            if (catNum.ElementAt(5) == 'A')
            {
                type = "TRILLIANT CELL READER";
            }
            if (catNum.ElementAt(5) == 'C')
            {
                type = "TRILLIANT SECURE MESH";
            }
            if (catNum.ElementAt(5) == 'D')
            {
                type = "SENSUS FLEXNET";
            }
            if (catNum.ElementAt(5) == 'E')
            {
                type = "SMARTSYNCH(ssi)-ITRON";
            }
            if (catNum.ElementAt(5) == 'F')
            {
                type = "TRILLIANT RPMA";
            }
           
            if (catNum.ElementAt(5) == 'H')
            {
                type = "ACLARA RF";
            }
          
            if (catNum.ElementAt(5) == 'M')
            {
                type = "ACLARA CELLULAR METRUM";
            }
            if (catNum.ElementAt(5) == 'S')
            {
                type = "SENSUS BRAND METER";
            }

            return type;

        }

        public string  MeterType()
        {
            string type = "";
            if (catNum.ElementAt(2) == '1')
            {
                type = "kv2c+ S-Base";
            }
            if (catNum.ElementAt(2) == '7')
            {
                type = "kv2/kv2c S-Base";
            }
            if (catNum.ElementAt(2) == 'A')
            {
                type = "kv2c GEN 5";
            }
            return type;
        }


        public string BaseType()
        {
            string type = "";
            if(catNum.ElementAt(4) == '0')
            {
                type = "Resi(2S)";
            }
            if (catNum.ElementAt(4) == '1')
            {
                type = "12S(25S)";
            }
            if (catNum.ElementAt(4) == '2')
            {
                type = "2S(1S)";
            }
            if (catNum.ElementAt(4) == '3')
            {
                type = "3S/4S";
            }
            if (catNum.ElementAt(4) == '4')
            {
                type = "16S";
            }
            if (catNum.ElementAt(4) == '5')
            {
                type = "45S";
            }
            if (catNum.ElementAt(4) == '6')
            {
                type = "36S";
            }
            if (catNum.ElementAt(4) == '7')
            {
                type = "56S";
            }
            if (catNum.ElementAt(4) == '9')
            {
                type = "9S";
            }

            return type;
        }

        public string OptBrd()
        {
            string type = "";

            if (catNum.ElementAt(6) == '1')
            {
                type = "Basic I/O";
            }
            if (catNum.ElementAt(6) == '4')
            {
                type = "Revenue Guard(RG)";
            }
            if (catNum.ElementAt(6) == '5')
            {
                type = "RG & Basic I/O";
            }
            if (catNum.ElementAt(6) == '8')
            {
                type = "Multiple I/O";
            }
            if (catNum.ElementAt(6) == '9')
            {
                type = "RG & Multipe I/O";
            }
            if (catNum.ElementAt(6) == 'U')
            {
                type = "UL RECONIZED";
            }
            if (catNum.ElementAt(6) == 'V')
            {
                type = "Basic I/O & UL RECONIZED";
            }
            if (catNum.ElementAt(6) == 'W')
            {
                type = "Multiple I/O & UL RECONIZED";
            }
            if (catNum.ElementAt(6) == 'R')
            {
                type = "Remote Disconnect";
            }

            return type;
        }

         public TimeSpan MeterTime()
         {
            

            if (resultge.Count > 0)
            {

                var x = resultge[0];
                if (x.status.Trim() == "P")
                {
                    return matchPoint.Subtract(x.check_time);
                }

                else
                {
                    return TimeSpan.Zero;
                }
               
            }
            else
            {
                return TimeSpan.Zero;
            }
         }

        public List<Pack1> PackcheckResults()
        {
            
            foreach (var x in result)
            {

                if (x.ge_serial_no.Trim() == geNum)
                {
                    resultge.Add(x);
                }
            }

            resultge.OrderByDescending(x => x.check_time);
            return resultge;
        }

        public List<double> AveMeterTime()
        {
            int start = 0;
            string placeHolder = " ";
            result.OrderByDescending(x => x.ge_serial_no);
            result2 = DatabaseHelper.MatchPointData2(catNum);
            result2.OrderByDescending(x => x.ID_GE_Serial_No);
            
            // Console.WriteLine(result2.Count + "Length of mp");
            // Console.WriteLine(result.Count + "Length of pack");
            foreach (var x in result)
            {
                if (x.ge_serial_no.Trim()!= placeHolder)
                {
                    placeHolder = x.ge_serial_no.Trim();

                    for (int i = start; i < result2.Count(); i++)
                    {
                        var y = result2[i];
                        //Console.WriteLine(i);
                        if (y.ID_GE_Serial_No.Trim() == placeHolder)
                        {
                            // Console.WriteLine(x.check_time + "time pack");
                            //Console.WriteLine(i);
                            times.Add(Math.Abs((x.check_time.Subtract(y.Match_Point).TotalMinutes)));
                            //times.Sort();
                            start = i;
                            break;
                        }
                    }
                }
            }
            times.Sort();
            return times;
        }

       



    }

}





    


