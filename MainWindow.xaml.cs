using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kv_repair
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        public static MainWindow mw;
        string number;
        string catNum;
        string geNum;
        int index2;

        int index4;
        int index5;
        int count;
        string type;
        bool sameTime;
        string error;
        bool timerON;

        DispatcherTimer timer;

        List<QueryInput> query1;
        List<MaterialList> query2;
        List<Pack1> packCheck;
        List<Set> query3;
        List<Cal> query4;
        List<Error> query5;
        List<ErrorCat> query6;
        List<PackError> query7;
        List<Solution> res;

        List<Sensus> amr1;
        List<Metrum> amr2;
        List<SFRN> amr3;
        List<OnRamp> amr4;
        List<Itron1> amr5;
        List<Ttrilliant_sm> amr6;
        List<Ttrilliant_cr> amr7;
        List<Umt> amr8;
        List<SSN> amr9;
        List<CellNet> amr10;
        List<Itron2> amr11;

        List<TextBlock> meter;
        List<TextBlock> calStart;
        List<TextBlock> step;
        List<TextBlock> wa;
        List<TextBlock> va;
        List<TextBlock> wb;
        List<TextBlock> vb;
        List<TextBlock> wc;
        List<TextBlock> vc;
        List<TextBlock> ws;

        List<int> Y = new List<int>();
        List<int> X = new List<int>();

        CAT obj;

        DateTime MPDate;
        public ObservableCollection<ListViewItem> GE { get; set; }
        public ObservableCollection<ListViewItem> MP { get; set; }
        public ObservableCollection<ListViewItem> CAT { get; set; }
        public ObservableCollection<ListViewItem> MPCell { get; set; }
        public ObservableCollection<ListViewItem> BME { get; set; }
        public ObservableCollection<ListViewItem> MMA { get; set; }
        public ObservableCollection<ListViewItem> OPT1 { get; set; }
        public ObservableCollection<ListViewItem> Serial1 { get; set; }
        public ObservableCollection<ListViewItem> OPT2 { get; set; }
        public ObservableCollection<ListViewItem> Serial2 { get; set; }
        #endregion
        public MainWindow()

        {
            InitializeComponent();
            mw = this;
            GE = new ObservableCollection<ListViewItem> { };
            MP = new ObservableCollection<ListViewItem> { };
            CAT = new ObservableCollection<ListViewItem> { };
            MPCell = new ObservableCollection<ListViewItem> { };
            BME = new ObservableCollection<ListViewItem> { };
            MMA = new ObservableCollection<ListViewItem> { };
            OPT1 = new ObservableCollection<ListViewItem> { };
            Serial1 = new ObservableCollection<ListViewItem> { };
            OPT2 = new ObservableCollection<ListViewItem> { };
            Serial2 = new ObservableCollection<ListViewItem> { };

            DataContext = this;

            NextButton.IsEnabled = false;
            PrevButton.IsEnabled = false;
            Next.IsEnabled = false;
            Previous.IsEnabled = false;
            NextAMR.IsEnabled = false;
            PreviousAMR.IsEnabled = false;

            meter = new List<TextBlock> { meterSerial4, meterSerial3, meterSerial2, meterSerial1 };
            calStart = new List<TextBlock> { calStart4, calStart3, calStart2, calStart1 };
            step = new List<TextBlock> { testStep4, testStep3, testStep2, testStep1 };
            wa = new List<TextBlock> { wA4, wA3, wA2, wA1 };
            va = new List<TextBlock> { VA4, VA3, VA2, VA1, };
            wb = new List<TextBlock> { wB4, wB3, wB2, wB1 };
            vb = new List<TextBlock> { VB4, VB3, VB2, VB1 };
            wc = new List<TextBlock> { wC4, wC3, wC2, wC1 };
            vc = new List<TextBlock> { VC4, VC3, VC4, VC4 };
            ws = new List<TextBlock> { wS4, wS3, wS2, wS1 };

            amr1 = new List<Sensus> { } ;
            amr2 = new List<Metrum> { };
            amr3 = new List<SFRN> { };
            amr4 = new List<OnRamp> { };
            amr5 = new List<Itron1> { };
            amr6 = new List<Ttrilliant_sm> { };
            amr7 = new List<Ttrilliant_cr> { };
            amr8 = new List<Umt> { };
            amr9 = new List<SSN> { };
            amr10 = new List<CellNet> { };
            amr11 = new List<Itron2>{};

            query1 = new List<QueryInput> { };
            query2 = new List<MaterialList> { };
            packCheck = new List<Pack1> { } ;
            query3 = new List<Set> { };
            query4 = new List<Cal> { };
            query5 = new List<Error> { };
            query6 = new List<ErrorCat> { };
            query7 = new List<PackError> { };
            res = new List<Solution> { };
            Y = new List<int>();
            X = new List<int>();
        }


        public bool Wait()
        {
            Thread.Sleep(2000);

            return false;
        }

        private void SerialNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SerialNumber.Text.Length >= 8)
            {
                Clear();
                Clear2();
                ClearChart();
                count = 0;
               

                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += Bg_DoWork;
                bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
                bg.RunWorkerAsync();
            }
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);


        }
        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (count == 0)
            {
                Launch();
            }

        }
        public void Launch()
        {
            NextButton.IsEnabled = false;
            PrevButton.IsEnabled = false;
            Next.IsEnabled = false;
            Previous.IsEnabled = false;
            NextAMR.IsEnabled = false;
            PreviousAMR.IsEnabled = false;
            Thinking.Text = "Executing Queries...";


            count++;
            number = SerialNumber.Text;
            input.Text = "Search: "+number;
            SerialNumber.Text = "";
            number = number.Trim();
            BackgroundWorker bg11 = new BackgroundWorker();
            bg11.DoWork += Bg11_DoWork;
            bg11.RunWorkerCompleted += Bg11_RunWorkerCompleted;
            bg11.RunWorkerAsync();


         
        }
        public void GetValues2()
        {
            int locIndex = 0;
            sameTime = false;
            if (query3.Count > 1)
            {
                NextButton.IsEnabled = true;
                PrevButton.IsEnabled = true;
            }
            if (index2 == (query3.Count - 1))
            {

                NextButton.IsEnabled = false;
            }
            if (index2 == 0)
            {
                PrevButton.IsEnabled = false;
            }

            if (query3 != null && query3.Count>0)
            {
                int loop = 0;
                Clear2();
                var x = query3[index2];
                serial.Text = x.ID_SERIAL_NO;
                startDate.Text = x.TMSTMP_TST_START.ToString();
                endDate.Text = x.MSTMP_TST_END.ToString();
                catGUI.Text = x.ID_CAT_NO;
                PF.Text = x.CD_CAL_PASS;
                RR1.Text = x.CD_CAL_REASON1;
                RR2.Text = x.CD_CAL_REASON2;
                RR3.Text = x.CD_CAL_REASON3;
                WECO.Text = x.ID_STN_NO;
                if (x.CD_CAL_REASON1 != null)
                {
                    error = x.CD_CAL_REASON1.Trim();
                    SolGUI();
                }
                if (x.CD_CAL_PASS.Trim() == "N")
                {
                    PF.Foreground = Brushes.Red;

                }
                if (x.CD_CAL_PASS.Trim() == "Y")
                {
                    PF.Foreground = Brushes.Green;

                }
                if (x.ID_CAT_NO.Trim() != catNum)
                {
                    catGUI.Foreground = Brushes.Red;
                }
                if (x.ID_SERIAL_NO.Trim() != geNum)
                {
                    serial.Foreground = Brushes.Red;
                }

                if (query4.Count >0)
                {
                    
                    int count = 0;
                    foreach (var y in query4)
                    {
                        if (y.TMSTMP_TST_START == x.TMSTMP_TST_START)
                        {
                            locIndex = count;
                            sameTime = true;
                            break;

                        }
                        count++;
                    }

                    if (sameTime == true)
                    {
                        foreach (var y in query4)
                        {
                            if (y.TMSTMP_TST_START == x.TMSTMP_TST_START)
                            {
                                ++loop;

                            }
                        }
                        //Console.WriteLine(loop+" loop");
                        for ( int i= 0; i <loop; i++)
                        {
                                if (query4.Count > locIndex + i)
                                {


                                    var y = query4[locIndex + i];
                                    /*if(y.CD_STEP_NM.Trim() != "FL" && i == 0)
                                    {
                                        break;
                                    }
                                    if (y.CD_STEP_NM.Trim() != "LAG" && i == 1)
                                    {
                                        break;
                                    }
                                    if (y.CD_STEP_NM.Trim() != "PHSE" && i == 2)
                                    {
                                        break;
                                    }
                                    if (y.CD_STEP_NM.Trim() != "LL" && i == 3)
                                    {
                                        break;
                                    }*/


                                    meter[i].Text = y.ID_SERIAL_NO.Trim();
                                    if (meter[i].Text != geNum)
                                    {
                                        meter[i].Foreground = Brushes.Red;
                                    }


                                    calStart[i].Text = y.TMSTMP_TST_START.ToString().Trim();
                                    step[i].Text = y.CD_STEP_NM.Trim();

                                    wa[i].Text = y.VL_TST_W_A.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        wa[i].Foreground = Brushes.Red;
                                    }
                                    else
                                    {
                                        wa[i].Foreground = Brushes.Green;
                                    }

                                    va[i].Text = y.VL_TST_V2_A.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        va[i].Foreground = Brushes.Red;
                                    }

                                    else
                                    {
                                        va[i].Foreground = Brushes.Green;
                                    }
                                    wb[i].Text = y.VL_TST_W_B.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        wb[i].Foreground = Brushes.Red;
                                    }

                                    else
                                    {
                                        wb[i].Foreground = Brushes.Green;
                                    }

                                    vb[i].Text = y.VL_TST_V2_B.ToString().Trim();

                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        vb[i].Foreground = Brushes.Red;
                                    }
                                    else
                                    {
                                        vb[i].Foreground = Brushes.Green;
                                    }
                                    wc[i].Text = y.VL_TST_W_C.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        wc[i].Foreground = Brushes.Red;
                                    }

                                    else
                                    {
                                        wc[i].Foreground = Brushes.Green;
                                    }
                                    vc[i].Text = y.VL_TST_V2_C.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        vc[i].Foreground = Brushes.Red;
                                    }

                                    else
                                    {
                                        vc[i].Foreground = Brushes.Green;
                                    }

                                    ws[i].Text = y.VL_SERIES_WH.ToString().Trim();
                                    if (y.VL_TST_V2_A > 100.137 || y.VL_TST_V2_A < 99.893)
                                    {
                                        ws[i].Foreground = Brushes.Red;
                                    }

                                    else
                                    {
                                        ws[i].Foreground = Brushes.Green;
                                    }
                                }

                            }


                    }
                    else
                    {

                        Clear2();
                    }

                }


            }
        }

        public void GetValues1()
        {
            int count2 = 0;
            string cat = "";
            string bme = "";
            string mma = "";
            string opt1 = "";
            string opt2 = "";

            foreach (var x in query1)
            {

                GE.Add(new ListViewItem());
                GE[count2].Content = x.ID_GE_Serial_No.Trim();
                MP.Add(new ListViewItem());
                MP[count2].Content = x.Match_Point.ToString().Trim();
                CAT.Add(new ListViewItem());
                CAT[count2].Content = x.ID_CAT_NO.Trim();
                MPCell.Add(new ListViewItem());
                MPCell[count2].Content = x.Station_ID.Trim().Trim();
                BME.Add(new ListViewItem());
                BME[count2].Content = x.ID_BME_Scan_1.Trim();
                MMA.Add(new ListViewItem());
                MMA[count2].Content = x.ID_Reg_Scan_1.Trim();
                OPT1.Add(new ListViewItem());
                OPT1[count2].Content = x.ID_Brd_Scan_1.Trim();
                Serial1.Add(new ListViewItem()) ;
                Serial1[count2].Content = x.ID_Brd_No1_SN_1.Trim();
                OPT2.Add(new ListViewItem());
                OPT2[count2].Content = x.ID_Brd_Scan_2.Trim();
                Serial2.Add(new ListViewItem());
                Serial2[count2].Content = x.ID_Brd_No2_SN_1.Trim();

                cat = x.ID_CAT_NO.Trim();
                bme = x.ID_BME_Scan_1.Trim();
                mma = x.ID_Reg_Scan_1.Trim();
                opt1 = x.ID_Brd_Scan_1.Trim();
                opt2 = x.ID_Brd_Scan_2.Trim();
                
                if (count2 == 0)
                {
                    
                    MPDate = x.Match_Point;

                    ChangeColor(catNum, bme, mma, opt1, opt2);
                }

                if (count2 > 0)
                {
    
                    KeepColor(cat, bme, mma, opt1, opt2, count2);
                }
                count2++;

            }
        }

        public void ChangeColor(string cat2, string bme2, string mma2, string opt1_2, string opt2_2)
        {
            
            foreach (var y in query2)
            {

                mat1.Text = y.ID_CAT_NO;
                mat2.Text = y.ID_BME_NO;

                mat3.Text = y.ID_BRD_NO1;

                mat4.Text = y.ID_BRD_NO2;

                mat5.Text = y.ID_REG;

                mat6.Text = y.ID_CUST_PROG_NO;


                if (cat2.Contains(y.ID_CAT_NO.Trim()) == false)
                {


                    var tt = CAT[0];
                    
                    tt.Foreground  = Brushes.Red;

                    mat1.Foreground = Brushes.Red;
                }

                if (mma2.Contains(y.ID_REG.Trim().Substring(6)) == false && mma2.Contains(y.ID_REG.Trim().Substring(0, 7)) == false)
                {
                    var tt = MMA[0];

                    tt.Foreground = Brushes.Red;
                    mat5.Foreground = Brushes.Red;
                }
                if (opt1_2.Contains(y.ID_BRD_NO1.Trim()) == false)
                {
                    var tt = OPT1[0];

                    tt.Foreground = Brushes.Red;
                    mat3.Foreground = Brushes.Red;
                }
                if (opt2_2.Contains(y.ID_BRD_NO2.Trim()) == false)
                {
                    var tt = OPT2[0];

                    tt.Foreground = Brushes.Red;
                    mat4.Foreground = Brushes.Red;
                }

                if (bme2.Contains(y.ID_BME_NO.Trim()) == false)
                {
                    var tt = BME[0];

                    tt.Foreground = Brushes.Red;

                    mat2.Foreground = Brushes.Red;
                }
            }

        }
        public void KeepColor(string cat1, string bme1, string mma1, string opt1_1, string opt2_1, int index)
        {

            foreach (var y in query2)
            {



                if (cat1.Contains(y.ID_CAT_NO.Trim()) == false)
                {
                    var tt = CAT[index];

                    tt.Foreground = Brushes.Red;

                }

                if (mma1.Contains(y.ID_REG.Trim().Substring(6)) == false && mma1.Contains(y.ID_REG.Trim().Substring(0, 7)) == false)
                {
                    var tt = MMA[index];

                    tt.Foreground = Brushes.Red;

                }
                if (opt1_1.Contains(y.ID_BRD_NO1.Trim()) == false)
                {
                    var tt = OPT1[index];

                    tt.Foreground = Brushes.Red;

                }
                if (opt2_1.Contains(y.ID_BRD_NO2.Trim()) == false)
                {
                    var tt = OPT2[index];

                    tt.Foreground = Brushes.Red;

                }


                if (bme1.Contains(y.ID_BME_NO.Trim()) == false)
                {
                    var tt = BME[index];

                    tt.Foreground = Brushes.Red;

                }
            }

        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = true;

            if (query3.Count > 1 && index2 > 0)
            {
                --index2;
                GetValues2();


            }

            // GetValues2();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PrevButton.IsEnabled = true;


            if (query3.Count > 1 && index2 < (query3.Count - 1))
            {
                ++index2;
                GetValues2();

            }

        }

        public void Clear()
        {
            query1.Clear();
            query2.Clear();
            query3.Clear();
            query4.Clear();
            query5.Clear();
            query6.Clear();
            query7.Clear();
            packCheck.Clear();

            amr1.Clear();
            amr2.Clear();
            amr3.Clear();
            amr4.Clear();
            amr5.Clear();
            amr6.Clear();
            amr7.Clear();
            amr8.Clear();
            amr9.Clear();
            amr10.Clear();
            amr11.Clear();

            index2 = 0;
            index4 = 0;
            index5 = 0;

            catNum = "";
            geNum = "";
            mat1.Text = "";
            mat2.Text = "";
            mat3.Text = "";
            mat5.Text = "";
            mat6.Text = "";
            serial.Text = "";
            startDate.Text = "";
            endDate.Text = "";
            catGUI.Text = "";
            PF.Text = "";
            RR1.Text = "";
            RR2.Text = "";
            RR3.Text = "";
            WECO.Text = "";


            GE.Clear();
            MP.Clear();
            CAT.Clear();
            MPCell.Clear();
            BME.Clear();
            MMA.Clear();
            OPT1.Clear();
            Serial1.Clear();
            OPT2.Clear();
            Serial2.Clear();


            amrtype.Text = "";
            AMRSerial.Text = "";
            GESerial.Text = "";
            CS.Text = "";
            StatusAMR.Text = "";
            time.Text = "";
            Reason.Text = "";

            ge.Text = "";
            amr.Text = "";
            cat.Text = "";
            time.Text = "";
            status.Text = "";
            rej.Text = "";
            station.Text = "";
            bar.Text = "";
            timePCK.Text = "";

            MPtoPCK.Text = "";

            meterType.Text = " ";
            baseType.Text = " ";
            brd.Text = " ";

           
            errorCode.Text = "";
            desc.Text = "";
            sol1.Text = "";
            sol2.Text = "";
            sol3.Text = "";

            input.Text = "";

            Y.Clear();
            X.Clear();

            if(timerON == true)
            {
                timer.Stop();
                MPtoPCK.Text = "";
                timerON = false;
            }
        }

        public void Clear2()
        {


            for (int i = 0; i < 4; i++)
            {
                meter[i].Text = "";
                meter[i].Foreground = Brushes.Black;

                calStart[i].Text = "";
                calStart[i].Foreground = Brushes.Black;

                step[i].Foreground = Brushes.Black;
                step[i].Text = "";

                wa[i].Text = "";
                wa[i].Foreground = Brushes.Black;

                va[i].Text = "";
                va[i].Foreground = Brushes.Black;

                wb[i].Text = "";
                wb[i].Foreground = Brushes.Black;

                wb[i].Text = "";
                wb[i].Foreground = Brushes.Black;

                vb[i].Text = "";
                vb[i].Foreground = Brushes.Black;

                wc[i].Text = "";
                wc[i].Foreground = Brushes.Black;

                vc[i].Text = "";
                vc[i].Foreground = Brushes.Black;

                ws[i].Text = "";
                ws[i].Foreground = Brushes.Black;



            }
        }

        void GetValues3()
        {


            if (packCheck.Count > 1)
            {
                Next.IsEnabled = true;
                Previous.IsEnabled = true;
            }
            if (index4 == (packCheck.Count - 1))
            {

                Next.IsEnabled = false;
            }
            if (index4 == 0)
            {
                PrevButton.IsEnabled = false;
            }

            if (packCheck != null && packCheck.Count>0)
            {

                var x = packCheck[index4];
                ge.Text = x.ge_serial_no.Trim();
                amr.Text = x.amr_serial_no.Trim();
                cat.Text = x.catalog_no.Trim();
                timePCK.Text = x.check_time.ToString().Trim();
                status.Text = x.status.Trim();
                rej.Text = x.reason_rej.Trim();
                station.Text = x.workstation.Trim();
                bar.Text = x.meter_bar_code.Trim();

                if (status.Text == "P")
                {
                    status.Foreground = Brushes.Green;
                }

                if (status.Text == "F")
                {
                    status.Foreground = Brushes.Red;
                }
            }


        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Previous.IsEnabled = true;

            if (packCheck.Count > 1 && index4 < (packCheck.Count - 1))
            {
                ++index4;
                GetValues3();

            }

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            Next.IsEnabled = true;

            if (packCheck.Count > 1 && index4 > 0)
            {
                --index4;
                GetValues3();


            }
        }

        public void AMRType()
        {

            type = obj.TypeofAMR();


            if (type == "SENSUS BRAND METER" || type == "SENSUS FLEXNET")
            {

                amr1 = DatabaseHelper.Sensus(geNum);

                
            }

            if (type == "ACLARA CELLULAR METRUM")
            {
                amr2 = DatabaseHelper.Metrum(geNum);
                
            }

            if (type == "ACLARA RF")
            {
                amr3 = DatabaseHelper.SFRN(geNum);
                
            }
            if (type == "TRILLIANT RPMA")
            {
                amr4 = DatabaseHelper.OnRamp(geNum);
                
            }

            if (type == "SMARTSYNCH(ssi)-ITRON")
            {
                amr5 = DatabaseHelper.Itron1(geNum);
                
            }
            if (type == "TRILLIANT SECURE MESH")
            {
                amr6 = DatabaseHelper.Ttrilliant_sm(geNum);
                
            }

            if (type == "TRILLIANT CELL READER")
            {
                amr7 = DatabaseHelper.Ttrilliant_cr(geNum);
                
            }

            if (type == "UMT")
            {
                amr8 = DatabaseHelper.Umt(geNum);
                
            }

            if (type == "SSN")
            {
                //Console.WriteLine("SSN Meter");
                amr9 = DatabaseHelper.SSN(geNum);
                
            }

            if (type == "CELLNET/L&G")
            {
                amr10 = DatabaseHelper.CellNet(geNum);
              
            }

            if (type == "53ESS-ERT-ITRON")
            {
                amr11 = DatabaseHelper.Itron2(geNum);
                
            }

            


        }

        public void AMRGUI()
        {

            if (amr1 != null && amr1.Count > 0)
            {


                if (amr1.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr1.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr1[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;
                CS.Text = x.customer_serial_no;
                StatusAMR.Text = x.status;
                time.Text = x.cal_test_start_time;
                Reason.Text = x.reject_code;
                if (x.reject_code != null)
                {
                    error = x.reject_code.Trim();
                    SolGUI();
                }

                if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr2 != null && amr2.Count > 0)
            {


                if (amr2.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr2.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr2[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.IMEI;
                GESerial.Text = x.ge_serial_no;
                CS.Text = x.customer_serial_no;
                StatusAMR.Text = x.status;
                time.Text = x.weco_cal_test_start_time;
                Reason.Text = x.reject_code;

                if (x.reject_code != null)
                {
                    error = x.reject_code.Trim();
                    SolGUI();
                }

                if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr3 != null && amr3.Count > 0)
            {


                if (amr3.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr3.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr3[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_sn;
                GESerial.Text = x.meter_sn;
                CS.Text = x.customer_sn;
                StatusAMR.Text = x.status;
                time.Text = x.cal_test_start_time;
                Reason.Text = x.reject_code;
                if (x.reject_code != null)
                {
                    error = x.reject_code.Trim();
                    SolGUI();
                }

                if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr4.Count > 0)
            {


                if (amr4.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr4.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr4[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;

                StatusAMR.Text = x.status;
                time.Text = x.test_start_time;
                Reason.Text = x.error_code;

                if (x.error_code != null)
                {
                    error = x.error_code.Trim();
                    SolGUI();
                }
                    if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr5.Count > 0)
            {


                if (amr5.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr5.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr5[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.seccode;
                GESerial.Text = x.geserialno;
                CS.Text = x.customerserialno;
                StatusAMR.Text = x.icsteststatus;
                time.Text = x.testcompletetime;


                if (StatusAMR.Text.Trim() == "PASS")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "FAIL")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if ( amr6.Count > 0)
            {


                if (amr6.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr6.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr6[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;
                CS.Text = x.customer_serial_no;
                StatusAMR.Text = x.status;
                time.Text = x.cal_test_start_time;
                Reason.Text = x.reject_code;

                if (x.reject_code != null)
                {
                    error = x.reject_code.Trim();
                    SolGUI();
                }
                if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }


            if (amr7.Count > 0)
            {


                if (amr7.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr7.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr7[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;
                CS.Text = x.customer_serial_no;
                StatusAMR.Text = x.status;
                time.Text = x.cal_test_start_time;
                Reason.Text = x.reject_code;

                if (x.reject_code != null)
                {
                    error = x.reject_code.Trim();
                    SolGUI();
                }
                if (StatusAMR.Text.Trim() == "P")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "F")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if ( amr8.Count > 0)
            {
                //.WriteLine("amr8 here");

                if (amr8.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr8.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr8[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.dcsi_amr_no + x.dcsi_serial_no;
                GESerial.Text = x.ge_serial_no;
                CS.Text = x.company_no;
                StatusAMR.Text = x.calibration_pass;
                time.Text = x.test_date;


                if (StatusAMR.Text.Trim() == "Y")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "N")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr9.Count > 0)
            {


                if (amr9.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr9.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr9[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.ncc_card_barcode;
                GESerial.Text = x.ge_serial_no;

                StatusAMR.Text = x.calibration_pass_fail_status;
                time.Text = x.calibration_date;


                if (StatusAMR.Text.Trim()== "Pass")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "Fail")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if (amr10.Count > 0)
            {


                if (amr10.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr10.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr10[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;

                StatusAMR.Text = x.pass_fail_status;
                time.Text = x.tmstmp_tst_end;
                CS.Text = x.id_cust;


                if (StatusAMR.Text.Trim() == "Passed")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "Failed")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }

            if ( amr11.Count > 0)
            {


                if (amr11.Count > 1)
                {
                    NextAMR.IsEnabled = true;
                    PreviousAMR.IsEnabled = true;
                }
                if (index5 == (amr11.Count - 1))
                {

                    NextAMR.IsEnabled = false;
                }
                if (index5 == 0)
                {
                    PreviousAMR.IsEnabled = false;
                }

                var x = amr11[index5];

                amrtype.Text = type;
                AMRSerial.Text = x.amr_serial_no;
                GESerial.Text = x.ge_serial_no;

                StatusAMR.Text = x.pass_fail_status;
                time.Text = x.tmstmp_tst_end;
                CS.Text = x.id_cust;


                if (StatusAMR.Text.Trim() == "Passed")
                {
                    StatusAMR.Foreground = Brushes.Green;
                }
                if (StatusAMR.Text.Trim() == "Failed")
                {
                    StatusAMR.Foreground = Brushes.Red;
                }

            }
            else
            {
                amrtype.Text = type;
            }


        }


        private void PreviousAMR_Click(object sender, RoutedEventArgs e)
        {
            --index5;
            NextAMR.IsEnabled = true;
            AMRGUI();
        }

        private void NextAMR_Click(object sender, RoutedEventArgs e)
        {
            ++index5;
            PreviousAMR.IsEnabled = true;
            AMRGUI();
        }

        public void MeterInfo()
        {
            meterType.Text = obj.MeterType();
            baseType.Text = obj.BaseType();
            brd.Text = obj.OptBrd();

            TimeSpan timespan = obj.MeterTime();


            if (timespan == TimeSpan.Zero)
            {

                StartClock();
            }

            else
            {

                MPtoPCK.Text = string.Format("{0:dd\\.hh\\:mm\\:ss}", timespan);
            }
        }

        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
            timerON = true;
            
        }

        void tickevent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MPtoPCK.Text = string.Format("{0:dd\\.hh\\:mm\\:ss}", DateTime.Now.Subtract(MPDate));
        }

        void TimeData()
        {
            //Console.WriteLine("here1");
            List<double> timeSort = obj.AveMeterTime();
            //Console.WriteLine("here1");


            int count10 = 0;
            double count10_2 = 10.0;
            int counter;
            if (timeSort.Count > 0)
            {
                int numBins = (int)Math.Ceiling((timeSort[timeSort.Count - 1] - timeSort[0]) / 10);
                //Console.WriteLine(numBins + " number of bins");
                //Console.WriteLine(timeSort.Count + " length timesort");
                //Console.WriteLine(timeSort[0] + " timesort min");
                //Console.WriteLine(timeSort[timeSort.Count - 1] + " mav timesort");

                for (int i = 0; i < numBins; i++)
                {
                    //Console.WriteLine("here2");
                    X.Add(count10);

                    counter = 0;
                    foreach (var x in timeSort)
                    {
                        if (x < count10_2 && x >= count10)
                        {
                            ++counter;

                        }
                        if (x >= count10_2)
                        {
                            Y.Add(counter);
                            break;
                        }
                    }
                    count10 += 10;
                    count10_2 += 10;

                }

                //Console.WriteLine("here2");
            }
        }

        void PlotData(List<int> y, List<int> x)
        {
            Dictionary<int, int> value;
            value = new Dictionary<int, int>();
            for (int i = 0; i < x.Count; i++)
            {
                value.Add(y[i], x[i]);
            }
            //Console.WriteLine("here3");
            //Console.WriteLine(x.Min());
            Chart chart = this.FindName("MyWinformChart") as Chart;
            chart.DataSource = value;

            chart.Series["series"].XValueMember = "Key";
            chart.Series["series"].YValueMembers = "value";
            chart.ChartAreas[0].RecalculateAxesScale();
            chart.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart.ChartAreas[0].AxisX.Title = "Time: Minutes";
            chart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 20);
            chart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 15);
            chart.ChartAreas[0].AxisY.Title = "Frequency";
            chart.Titles.Clear();
            chart.Titles.Add("Time per Meter per Cat NO: " + catNum);

            Thinking.Text = "";
        }

        public void ClearChart()
        {
            List<int> value = new List<int>();
            value.Add(0);
            Chart chart = this.FindName("MyWinformChart") as Chart;
            chart.DataSource = value;

            MyWinformChart2.Legends.Clear();
            MyWinformChart.Series[0].Points.Clear();
            MyWinformChart2.Update();
            MyWinformChart2.Series[0].Points.Clear();
            MyWinformChart3.Series[0].Points.Clear();

            MyWinformChart3.Legends.Clear();

        }

        public void ErrorData()
        {

            MyWinformChart2.Series["series2"].Points.Clear();
            int errorcount = 0;
            MyWinformChart2.DataSource = null;
            MyWinformChart2.Legends.Clear();

            //MyWinformChart2.Series["series2"] = null;
            MyWinformChart2.Series["series2"].IsValueShownAsLabel = true;
            MyWinformChart2.Legends.Add(new Legend());
            MyWinformChart2.Update();

            
            foreach (var x in query5)
            {
                errorcount = 0;
                foreach (var y in query6)
                {

                    if (y.CD_CAL_REASON1 != null && x.CD_CAL_REASON1 != null)
                        if (x.CD_CAL_REASON1.Trim() == y.CD_CAL_REASON1.Trim())
                        {
                            ++errorcount;
                        }

                }
                if (errorcount > 0)
                {
                    MyWinformChart2.Series["series2"].Points.AddXY(x.CD_CAL_REASON1.Trim(), errorcount.ToString());
                }
            }
        }


        private void Bg2_DoWork(object sender, DoWorkEventArgs e)
        {

            packCheck = obj.PackcheckResults();

        }
        private void Bg2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            GetValues3();

        }

        private void Bg3_DoWork(object sender, DoWorkEventArgs e)
        {

            query5 = DatabaseHelper.Error(catNum);
            query6 = DatabaseHelper.ErrorCat(catNum);



        }
        private void Bg3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            ErrorData();
        }

        private void Bg4_DoWork(object sender, DoWorkEventArgs e)
        {

            query7 = DatabaseHelper.PackError(catNum);



        }
        private void Bg4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Pack();
        }

        public void Pack()
        {
            MyWinformChart3.Series["series3"].Points.Clear();
            int errorcount = 0;

            MyWinformChart3.Series["series3"].IsValueShownAsLabel = true;
            MyWinformChart3.Legends.Add(new Legend());
            MyWinformChart3.Update();
            foreach (var x in query7)
            {
                errorcount = 0;
                foreach (var y in obj.result)
                {

                    if (y.reason_rej != null && x.reason_rej != null)
                        if (x.reason_rej.Trim() == y.reason_rej.Trim())
                        {
                            ++errorcount;
                        }

                }
                if (errorcount > 0)
                {
                    MyWinformChart3.Series["series3"].Points.AddXY(x.reason_rej.Trim(), errorcount.ToString());
                }

            }
           
        }

        public void SolGUI()
        {
            BackgroundWorker bg10 = new BackgroundWorker();
            bg10.DoWork += Bg10_DoWork;
            bg10.RunWorkerCompleted += Bg10_RunWorkerCompleted;
            bg10.RunWorkerAsync();
        }

        private void Bg5_DoWork(object sender, DoWorkEventArgs e)
        {
            query3 = DatabaseHelper.T_Set(geNum);
            query4 = DatabaseHelper.T_Result(geNum);


        }
        private void Bg5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            GetValues2();
        }

        private void Bg6_DoWork(object sender, DoWorkEventArgs e)
        {

            obj = new CAT(catNum, geNum, MPDate);

        }
        private void Bg6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            BackgroundWorker bg2 = new BackgroundWorker();
            bg2.DoWork += Bg2_DoWork;
            bg2.RunWorkerCompleted += Bg2_RunWorkerCompleted;
            bg2.RunWorkerAsync();

            BackgroundWorker bg7 = new BackgroundWorker();
            bg7.DoWork += Bg7_DoWork;
            bg7.RunWorkerCompleted += Bg7_RunWorkerCompleted;
            bg7.RunWorkerAsync();

            BackgroundWorker bg8 = new BackgroundWorker();
            bg8.DoWork += Bg8_DoWork;
            bg8.RunWorkerCompleted += Bg8_RunWorkerCompleted;
            bg8.RunWorkerAsync();

            BackgroundWorker bg9 = new BackgroundWorker();
            bg9.DoWork += Bg9_DoWork;
            bg9.RunWorkerCompleted += Bg9_RunWorkerCompleted;
            bg9.RunWorkerAsync();

            BackgroundWorker bg4 = new BackgroundWorker();
            bg4.DoWork += Bg4_DoWork;
            bg4.RunWorkerCompleted += Bg4_RunWorkerCompleted;
            bg4.RunWorkerAsync();
        }
        private void Bg7_DoWork(object sender, DoWorkEventArgs e)
        {

            AMRType();
        }
        private void Bg7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            AMRGUI();


        }
        private void Bg8_DoWork(object sender, DoWorkEventArgs e)
        {


        }
        private void Bg8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MeterInfo();
            

        }
        private void Bg9_DoWork(object sender, DoWorkEventArgs e)
        {

            TimeData();
        }
        private void Bg9_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)

        { 
            PlotData(X, Y);

        }

        private void Bg10_DoWork(object sender, DoWorkEventArgs e)
        {
            
            res = DatabaseHelper.Solution(error);

        }
        private void Bg10_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            errorCode.Text = "";
            desc.Text = "";
            sol1.Text = "";
            sol2.Text = "";
            sol3.Text = "";
            foreach (var x in res)
            {
                errorCode.Text = x.errcode;
                desc.Text = x.description;
                sol1.Text = x.Solution1;
                sol2.Text = x.Solution2;
                sol3.Text = x.Solution3;
            }

        }

        private void Bg11_DoWork(object sender, DoWorkEventArgs e)
        {
            query1 = DatabaseHelper.MatchPointData(number);
            query1.OrderByDescending(x => x.Match_Point);

            if (query1.Count > 0)
            {
                var w = query1[0];
                catNum = w.ID_CAT_NO.Trim();
                geNum = w.ID_GE_Serial_No.Trim();

                if(geNum.First() == 'R')
                {
                    geNum = geNum.Substring(1);
                }
                if (geNum.First() == 'X')
                {
                    geNum = geNum.Substring(1);
                }
                if (geNum.Last() == 'R')
                {
                   geNum = geNum.Remove(geNum.Length - 1);
                   
                }
               //Console.WriteLine(geNum);



                query2 = DatabaseHelper.PartList(catNum);
            }
        }
        private void Bg11_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GetValues1();
            if (geNum != "")
            {

                BackgroundWorker bg5 = new BackgroundWorker();
                bg5.DoWork += Bg5_DoWork;
                bg5.RunWorkerCompleted += Bg5_RunWorkerCompleted;
                bg5.RunWorkerAsync();

                BackgroundWorker bg6 = new BackgroundWorker();
                bg6.DoWork += Bg6_DoWork;
                bg6.RunWorkerCompleted += Bg6_RunWorkerCompleted;
                bg6.RunWorkerAsync();




                BackgroundWorker bg3 = new BackgroundWorker();
                bg3.DoWork += Bg3_DoWork;
                bg3.RunWorkerCompleted += Bg3_RunWorkerCompleted;
                bg3.RunWorkerAsync();




            }
            else
            {
                Thinking.Text = "";
                MessageBox.Show("NO Results Found");
            }


        }
    }
}
