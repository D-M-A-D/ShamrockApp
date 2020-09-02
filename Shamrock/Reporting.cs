using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Shamrock
{
    public class Reporting
    {
        #region declaration, fonts + colors
        private readonly BaseFont basefont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        enum OutputFormat { Number, Number1, Number2, Number4, Percent, PercentLeft, Text, TextRight, TextMid, Date, DateTime };

        System.Globalization.CultureInfo clientNumberFormat = new System.Globalization.CultureInfo("de-CH");
        Boolean useGermanNumberSeparators = false;

        List<int> uniqueColors = new List<int>();
        List<int> useWhiteText = new List<int>();
        iTextSharp.text.Font fontArial; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GREEN)); // = FontFactory.GetFont("Arial", iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fontArialBold; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GREEN));

        iTextSharp.text.Font fontDocHeader; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(ColorCodHeaderFont)));
        iTextSharp.text.Font fontDocSubHeader; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(ColorCodHeaderFont)));
        iTextSharp.text.Font fontTableTitle; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(ColorTitleFont)));
        iTextSharp.text.Font fontTableTitleAlternative; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(ColorTitleFontAlternative)));
        iTextSharp.text.Font fontTableHeader; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(ColorTableHeaderFont)));
        iTextSharp.text.Font fontDetail; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(ColorDetailFont)));
        iTextSharp.text.Font fontDetailNeg; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(ColorDetailFontNeg)));
        iTextSharp.text.Font fontDetailWhite; // = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(System.Drawing.Color.White)));
        iTextSharp.text.Font fontTotal;

        BaseColor ColorLightBlue = new BaseColor(186, 232, 247);
        BaseColor ColorBlue = new BaseColor(51, 167, 204);
        BaseColor ColorGrey1 = new BaseColor(76, 76, 77);// unchecked((int)0x4C4C4D); // 4C4C4D //0x727273
        BaseColor ColorGrey2 = new BaseColor(204, 204, 204);//unchecked((int)0xCCCCCC);
        BaseColor ColorGrey3 = new BaseColor(227, 227, 227);//unchecked((int)0xE3E3E3);
        BaseColor ColorGrey4 = new BaseColor(235, 235, 235);//unchecked((int)0xebebeb);
        BaseColor ColorGrey5 = new BaseColor(245, 245, 245);//unchecked((int)0xF5F5F5); // F5F5F5

        void SetFonts()
        {
            fontArial = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GREEN)); // = FontFactory.GetFont("Arial", iTextSharp.text.Font.BOLD);
            fontArialBold = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GREEN));

            fontDocHeader = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 12, iTextSharp.text.Font.BOLD, ColorBlue));
            fontDocSubHeader = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 10, iTextSharp.text.Font.BOLD, BaseColor.LIGHT_GRAY));
            fontTableTitle = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, new BaseColor(50,59,62)));
            fontTableTitleAlternative = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, BaseColor.ORANGE));
            fontTableHeader = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, ColorBlue));
            fontDetail = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
            fontTotal = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
            fontDetailNeg = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, BaseColor.RED));
            fontDetailWhite = new iTextSharp.text.Font(FontFactory.GetFont("microsoftsansserif", 5, iTextSharp.text.Font.NORMAL, BaseColor.WHITE));
        }
        #endregion
        public Reporting()
        {
            SetFonts();
        }
        public void CreatePDF(Stream output, Compet c)
        {
            int pageN = 1;
            //Document document = new Document(PageSize.A4.Rotate(), 50, 50, 25, 25);
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();
                PdfContentByte contentbyte = writer.DirectContent;

                #region declaration
                float ctImageWidth = 0;
                float ctImageHeight = 0;
                float origImageWidth = 0;
                float origImageHeight = 0;

                float ctX = document.Left; // X Axis starting left wich is the value of the leftMargin
                float ctY = document.Top; // Y Axis starting at top which is page.height - topmargin) substract the height of object to progress towards bottom of the page
                float interstice = 10f;
                float ctColWidth = 100f;
                float ctRowHeight = 100f;
                float colWidth_1st = 250f;
                float colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;
                float colWidth_3nd = document.Right - document.Left - colWidth_1st - interstice;
                float topAfterHeader = 50f;
                Boolean isEvenLine = true;

                String sText = "";
                String shortText = "";
                PdfPTable ctPdfPTable = null;

                interstice = 10f;
                #endregion
                int nbOfPlayer = c.Players.Count();

                #region page WeekResults Overview
                CreatePDFHeaderAndFooter(document, null, "WeekResults Summary", "", contentbyte, pageN);

                colWidth_1st = 520;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_1st;
                ctX = document.Left;
                ctY = document.Top - topAfterHeader;
                #region 1st WeekResults

                List<float> colWidths = new List<float>();
                colWidths.Add(7f); //Players
                foreach (day ctDay in c.days)
                {
                    colWidths.Add(4f); //Mt
                    colWidths.Add(4f); //SD
                    if(c.configForYear.useExtra)
                        colWidths.Add(4f); //Ex
                    colWidths.Add(4f); //SW
                    colWidths.Add(4f); //Ef
                    colWidths.Add(4f); //Vi
                    colWidths.Add(4f); //R
                }

                ctPdfPTable = new PdfPTable(colWidths.ToArray());
                //ctPdfPTable = new PdfPTable(new float[] { 7f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                ctPdfPTable.TotalWidth = ctColWidth;
                isEvenLine = true;
                //Title
                ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Position after Round {0} (Mt=Sh.Points Match, SD=Sh.Points Stbl.Day, SW=Sh.Points Stbl.Week, Ef=Sh.Points Effective, Vi=Sh.Points Virtual", c.ctDayOfCompetition)));

                //Header
                AddHeaderCell((object)ctPdfPTable, "P");
                foreach (day ctDay in c.days)
                {
                    AddDetailCell((object)ctPdfPTable, "Mt");
                    AddDetailCell((object)ctPdfPTable, "D");
                    if (c.configForYear.useExtra)
                        AddDetailCell((object)ctPdfPTable, "Ex");
                    AddDetailCell((object)ctPdfPTable, "W");
                    AddDetailCell((object)ctPdfPTable, "Ef");
                    AddDetailCell((object)ctPdfPTable, "Vi");
                    AddHeaderCell((object)ctPdfPTable, "R" + ctDay.nr);
                }

                List<String> sortedPlayer = c.getSortedPlayer(c.ctDayOfCompetition);
                foreach (string playerName in sortedPlayer)
                {
                    Boolean showVirtual = false;
                    isEvenLine = false;
                    AddDetailCell((object)ctPdfPTable, playerName.Left(4), isEvenLine);
                    foreach (day ctDay in c.days)
                    {
                        if (!showVirtual && ctDay.stblPoints.isValidForStblDay())
                            showVirtual = true;

                        if (ctDay.courseDefinition == null)
                        {
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            if (c.configForYear.useExtra)
                                AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                        }
                        else
                        {
                            if(c.getResultsbyDayNr(ctDay.nr)[playerName].shMatch == 0)
                                AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                            else
                                AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shMatch, isEvenLine, OutputFormat.Number1);

                            if (c.getResultsbyDayNr(ctDay.nr)[playerName].shStblDay == 0)
                                AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                            else
                                AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shStblDay, isEvenLine, OutputFormat.Number1);

                            if (c.configForYear.useExtra)
                            {
                                if (c.getResultsbyDayNr(ctDay.nr)[playerName].shExtraDay == 0)
                                    AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                                else
                                    AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shExtraDay, isEvenLine, OutputFormat.Number1);
                            }

                            if (!showVirtual ||c.getResultsbyDayNr(ctDay.nr)[playerName].shStblWeek == 0)
                                AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                            else
                                AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shStblWeek, isEvenLine, OutputFormat.Number1);

                            if (c.getResultsbyDayNr(ctDay.nr)[playerName].shEffective == 0)
                                AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                            else
                                AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shEffective, isEvenLine, OutputFormat.Number1);

                            if (showVirtual)
                            {
                                if (c.getResultsbyDayNr(ctDay.nr)[playerName].shVirtual == 0)
                                    AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                                else
                                    AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shVirtual, isEvenLine, OutputFormat.Number1);
                            }
                            else
                            {
                                if (c.getResultsbyDayNr(ctDay.nr)[playerName].shEffective == 0)
                                    AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.Number);
                                else
                                    AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[playerName].shEffective, isEvenLine, OutputFormat.Number1);
                            }

                            //if (ctDay.nr == c.ctDay)
                                AddTotalCell((object)ctPdfPTable, String.Format("#{0}", c.getResultsbyDayNr(ctDay.nr)[playerName].posVirtual.ToString("N0")), isEvenLine);
                            //else
                            //    AddDetailCell((object)ctPdfPTable, String.Format("#{0}", c.getResultsbyDayNr(ctDay.nr)[playerName].posVirtual.ToString("N0")), isEvenLine);
                        }
                    }
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);
                #endregion

                #endregion
                #endregion
                #region page WeekResults Details
                CreatePDFHeaderAndFooter(document, null, "WeekResults Summary", "", contentbyte, pageN);

                colWidth_1st = 280;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_1st;
                ctX = document.Left;
                //ctY = document.Top - topAfterHeader - 150;
                ctY -= ctPdfPTable.TotalHeight + interstice;
                float YafterOverview = ctY;
                #region 1st WeekResults

                switch (nbOfPlayer)
                {
                    case 11:
                        ctPdfPTable = new PdfPTable(new float[] { 14f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 10:
                        ctPdfPTable = new PdfPTable(new float[] { 14f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 9:
                        ctPdfPTable = new PdfPTable(new float[] { 14f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 8:
                        ctPdfPTable = new PdfPTable(new float[] { 14f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 7:
                        ctPdfPTable = new PdfPTable(new float[] { 14f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                }
                ctPdfPTable.TotalWidth = ctColWidth;
                foreach (day ctDay in c.days)
                {
                    if (ctDay.courseDefinition == null)
                        break;
                    isEvenLine = true;
                    //Title
                    ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Rnd {0} ({7}): {1} ({4}, par:{6} cr:{2}, sr:{3}, Dist: {5})", new object[] { ctDay.nr, ctDay.courseDefinition.name, ctDay.courseDefinition.cr, ctDay.courseDefinition.sr, ctDay.courseDefinition.TeeColor, ctDay.courseDefinition.Yards, ctDay.courseDefinition.getSumPar(), ctDay.playModeDisplay })));

                    AddHeaderCell((object)ctPdfPTable, " ");
                    foreach (Player P in c.Players)
                    {
                        AddHeaderCell((object)ctPdfPTable, P.name.Left(4));
                    }

                    AddDetailCell((object)ctPdfPTable, "Sh.Points Match", isEvenLine);
                    foreach (Player P in c.Players)
                    {
                        AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[P.name].shMatch, isEvenLine, OutputFormat.Number1);
                    }
                    if (ctDay.stblPoints.isValidForStblDay())
                    {
                        isEvenLine = !isEvenLine;
                        AddDetailCell((object)ctPdfPTable, "Stbl.Day", isEvenLine);
                        foreach (Player P in c.Players)
                        {
                            int worstday = c.getResultsbyDayNr(c.ctDayOfCompetition)[P.name].worstDay; //taken at the ct day of competition (worst as of today, not each day)
                            if (c.configForYear.useScratch && worstday > 0 && ctDay.nr == worstday)
                                AddDetailCell((object)ctPdfPTable, $"#{c.getResultsbyDayNr(ctDay.nr)[P.name].posStblDay.ToString("N0")} - {c.getResultsbyDayNr(ctDay.nr)[P.name].StblDay}", isEvenLine, OutputFormat.TextMid, BackGroundColor: BaseColor.ORANGE);
                            else
                                AddDetailCell((object)ctPdfPTable, $"#{c.getResultsbyDayNr(ctDay.nr)[P.name].posStblDay.ToString("N0")} - {c.getResultsbyDayNr(ctDay.nr)[P.name].StblDay}", isEvenLine, OutputFormat.TextMid);
                        }
                        //AddDetailCell((object)ctPdfPTable, "Position Stbl.Day", isEvenLine);
                        //foreach (Player P in c.Players)
                        //{
                        //    AddDetailCell((object)ctPdfPTable, String.Format("#{0}", c.getResultsbyDayNr(ctDay.nr)[P.name].posStblDay.ToString("N0")), isEvenLine);
                        //}
                        isEvenLine = !isEvenLine;
                        AddDetailCell((object)ctPdfPTable, "Sh.Points Stbl.Day", isEvenLine);
                        foreach (Player P in c.Players)
                        {
                            AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[P.name].shStblDay, isEvenLine, OutputFormat.Number1);
                        }
                    }
                    if (ctDay.stblPoints.isValidForStblDay())
                    {
                        isEvenLine = !isEvenLine;
                        AddDetailCell((object)ctPdfPTable, "Stbl.Week", isEvenLine);
                        foreach (Player P in c.Players)
                        {
                            if (c.configForYear.useScratch)
                                AddDetailCell((object)ctPdfPTable, $"#{c.getResultsbyDayNr(ctDay.nr)[P.name].posStblWeek.ToString("N0")} - {c.getResultsbyDayNr(ctDay.nr)[P.name].StblWeek_X}", isEvenLine, OutputFormat.TextMid);
                            else
                                AddDetailCell((object)ctPdfPTable, $"#{c.getResultsbyDayNr(ctDay.nr)[P.name].posStblWeek.ToString("N0")} - {c.getResultsbyDayNr(ctDay.nr)[P.name].StblWeek}", isEvenLine, OutputFormat.TextMid);
                        }
                    }
                    //AddDetailCell((object)ctPdfPTable, "Position Stbl.Week", isEvenLine);
                    //foreach (Player P in c.Players)
                    //{
                    //    AddDetailCell((object)ctPdfPTable, String.Format("#{0}", c.getResultsbyDayNr(ctDay.nr)[P.name].posStblWeek.ToString("N0")), isEvenLine);
                    //}

                    isEvenLine = !isEvenLine;
                    AddDetailCell((object)ctPdfPTable, "Sh.Points Stbl.Week", isEvenLine);
                    foreach (Player P in c.Players)
                    {
                        AddDetailCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[P.name].shStblWeek, isEvenLine, OutputFormat.Number1);
                    }
                    AddTotalCell((object)ctPdfPTable, "Sh.Points Effective", isEvenLine);
                    foreach (Player P in c.Players)
                    {
                        AddTotalCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[P.name].shEffective, isEvenLine, OutputFormat.Number1);
                    }
                    AddTotalCell((object)ctPdfPTable, "Sh.Points Virtual", isEvenLine);
                    foreach (Player P in c.Players)
                    {
                        AddTotalCell((object)ctPdfPTable, c.getResultsbyDayNr(ctDay.nr)[P.name].shVirtual, isEvenLine, OutputFormat.Number1);
                    }
                    AddTotalCell((object)ctPdfPTable, "Position", isEvenLine);
                    foreach (Player P in c.Players)
                    {
                        AddTotalCell((object)ctPdfPTable, String.Format("#{0}", c.getResultsbyDayNr(ctDay.nr)[P.name].posVirtual.ToString("N0")), isEvenLine);
                    }

                    //EmptyRow
                    ctPdfPTable.AddCell(GetEmptyRow(ctPdfPTable.NumberOfColumns));
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);

                #endregion

                #endregion
                #endregion
                //document.NewPage(); ++pageN;
                #region page MatchResults
                //CreatePDFHeaderAndFooter(document, null, "MatchResults", "", contentbyte, pageN);

                colWidth_1st = 280;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_2nd;
                ctX = document.Left + colWidth_1st + interstice;
                ctY = YafterOverview;
                #region 1st Match Results

                ctPdfPTable = new PdfPTable(new float[] { 2f, 7f, 18, 4f});
                ctPdfPTable.TotalWidth = ctColWidth;
                foreach (day ctDay in c.days)
                {
                    if (ctDay.courseDefinition == null)
                        break;
                    isEvenLine = false;
                    //Title
                    ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Rnd {0} ({7}): {1} ({4}, par:{6} cr:{2}, sr:{3}, Dist: {5})", new object[] { ctDay.nr, ctDay.courseDefinition.name, ctDay.courseDefinition.cr, ctDay.courseDefinition.sr, ctDay.courseDefinition.TeeColor, ctDay.courseDefinition.Yards, ctDay.courseDefinition.getSumPar(), ctDay.playModeDisplay })));

                    AddHeaderCell((object)ctPdfPTable, "Flt");
                    AddHeaderCell((object)ctPdfPTable, "Type");
                    AddHeaderCell((object)ctPdfPTable, "Desc");
                    AddHeaderCell((object)ctPdfPTable, "Score");
                    //AddHeaderCell((object)ctPdfPTable, "TmW");
                    //AddHeaderCell((object)ctPdfPTable, "TmL ");
                    foreach (flight ctFlight in ctDay.flights.Values)
                    {
                        foreach (string kM in ctFlight.matchs.Keys)
                        {
                            AddDetailCell((object)ctPdfPTable, ctFlight.name, isEvenLine);
                            AddDetailCell((object)ctPdfPTable, ctFlight.matchType.ToString(), isEvenLine);
                            if (ctDay.matchScores.matchResults.ContainsKey(kM))
                            {
                                MatchScore ctMS = ctDay.matchScores.matchResults[kM];
                                match m = ctFlight.matchs[kM];
                                team WinnerTeam = null;
                                team LoserTeam = null;
                                if (ctMS.WinnerteamName.ToString() == "0")
                                {
                                    AddDetailCell((object)ctPdfPTable, string.Format("{0} vs. {1}", m.Team1.GetPlayersString(), m.Team2.GetPlayersString()), isEvenLine);
                                    AddDetailCell((object)ctPdfPTable, "DRAW", isEvenLine);
                                    //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                    //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                }
                                else if (ctMS.WinnerteamName.ToString() == "-1")
                                {
                                    AddDetailCell((object)ctPdfPTable, string.Format("{0} vs. {1}", m.Team1.GetPlayersString(), m.Team2.GetPlayersString()), isEvenLine);
                                    AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                    //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                    //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                }
                                else
                                {
                                    if (ctMS.WinnerteamName.ToString() == "1")
                                    {
                                        WinnerTeam = m.Team1;
                                        LoserTeam = m.Team2;
                                    }
                                    else
                                    {
                                        WinnerTeam = m.Team2;
                                        LoserTeam = m.Team1;
                                    }

                                    if (ctMS.nrPoints > 0)
                                    {
                                        AddDetailCell((object)ctPdfPTable, string.Format("{0} def. {1}", WinnerTeam.GetPlayersString(), LoserTeam.GetPlayersString()), isEvenLine);
                                        AddDetailCell((object)ctPdfPTable, string.Format("{0}&{1}", ctMS.nrPoints, ctMS.nrHolesLeft), isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, WinnerTeam.name1based, isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, LoserTeam.name1based, isEvenLine);
                                    }
                                    else if (ctMS.nrPoints < 0 || ctMS.nrHolesLeft < 0) //na
                                    {
                                        AddDetailCell((object)ctPdfPTable, string.Format("{0} def. {1}", WinnerTeam.GetPlayersString(), LoserTeam.GetPlayersString()), isEvenLine);
                                        AddDetailCell((object)ctPdfPTable, "n.a.", isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, WinnerTeam.name1based, isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, LoserTeam.name1based, isEvenLine);
                                    }
                                    else
                                    {
                                        AddDetailCell((object)ctPdfPTable, string.Format("{0} vs. {1}", WinnerTeam.GetPlayersString(), LoserTeam.GetPlayersString()), isEvenLine);
                                        AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                        //AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                                    }
                                }
                            }
                            else
                            {

                                AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                                AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                                //AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                                //AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                            }
                        }
                        isEvenLine = !isEvenLine;
                    }

                    //EmptyRow
                    ctPdfPTable.AddCell(GetEmptyRow(ctPdfPTable.NumberOfColumns));
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);

                #endregion

                #endregion
                #endregion
                document.NewPage(); ++pageN;
                #region page DailyScores
                CreatePDFHeaderAndFooter(document, null, "DailyScores and Stableford points", "", contentbyte, pageN);

                colWidth_1st = 500;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_1st;
                ctX = document.Left;
                ctY = document.Top - topAfterHeader;
                #region 1st DailyScores

                ctPdfPTable = new PdfPTable(new float[] { 4f, 3f, 3f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                ctPdfPTable.TotalWidth = ctColWidth;
                foreach (day ctDay in c.days)
                {
                    if (ctDay.courseDefinition == null)
                        break;
                    isEvenLine = false;
                    //Title
                    ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns - 5, String.Format("Rnd {0} ({7}): {1} ({4}, par:{6} cr:{2}, sr:{3}, Dist: {5})", new object[] { ctDay.nr, ctDay.courseDefinition.name, ctDay.courseDefinition.cr, ctDay.courseDefinition.sr, ctDay.courseDefinition.TeeColor, ctDay.courseDefinition.Yards, ctDay.courseDefinition.getSumPar(), ctDay.playModeDisplay })));
                    ctPdfPTable.AddCell(GetTitleCellAlternative(5, String.Format("")));

                    #region course par
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                    AddDetailCell((object)ctPdfPTable, "Par", isEvenLine, OutputFormat.Number2);
                    for (int i = 1; i <= 9; i++)
                        AddDetailCell((object)ctPdfPTable, ctDay.courseDefinition.getHolebyNr(i).par, isEvenLine, OutputFormat.Number);
                    AddTotalCell((object)ctPdfPTable, ctDay.courseDefinition.getSumParOut(), isEvenLine, OutputFormat.Number);
                    for (int i = 10; i <= 18; i++)
                        AddDetailCell((object)ctPdfPTable, ctDay.courseDefinition.getHolebyNr(i).par, isEvenLine, OutputFormat.Number);

                    AddTotalCell((object)ctPdfPTable, ctDay.courseDefinition.getSumParIn(), isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddTotalCell((object)ctPdfPTable, ctDay.courseDefinition.getSumPar(), isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    #endregion
                    #region course hcp
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine);
                    AddDetailCell((object)ctPdfPTable, "Hcp", isEvenLine, OutputFormat.Number2);
                    for (int i = 1; i <= 9; i++)
                        AddDetailCell((object)ctPdfPTable, ctDay.courseDefinition.getHolebyNr(i).hcp, isEvenLine, OutputFormat.Number);
                    AddTotalCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    for (int i = 10; i <= 18; i++)
                        AddDetailCell((object)ctPdfPTable, ctDay.courseDefinition.getHolebyNr(i).hcp, isEvenLine, OutputFormat.Number);

                    AddTotalCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddTotalCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    AddDetailCell((object)ctPdfPTable, " ", isEvenLine, OutputFormat.Number);
                    #endregion
                    #region Table Column Headers
                    AddHeaderCell((object)ctPdfPTable, "Player");
                    AddHeaderCell((object)ctPdfPTable, "hcpFix", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "Play", OutputFormat.TextRight);
                    for (int i = 1; i <= 9; i++)
                        AddHeaderCell((object)ctPdfPTable, i.ToString(), OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "O", OutputFormat.TextRight, null, ColorGrey5);
                    for (int i = 10; i <= 18; i++)
                        AddHeaderCell((object)ctPdfPTable, i.ToString(), OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "I", OutputFormat.TextRight, null, ColorGrey5);
                    AddHeaderCell((object)ctPdfPTable, "6", OutputFormat.TextRight, null, ColorGrey5);
                    AddHeaderCell((object)ctPdfPTable, "3", OutputFormat.TextRight, null, ColorGrey5);
                    AddHeaderCell((object)ctPdfPTable, "T", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "Pos", OutputFormat.TextRight, null, ColorGrey5);
                    #endregion
                    isEvenLine = !isEvenLine;
                    #region DetailsCells
                    if (ctDay.stblPoints.isValidForStblDay())
                    {
                        List<String> sortedPlayers = ctDay.stblPoints.getSortedPlayerList();
                        int cnt = 0;
                        foreach (String ctName in sortedPlayers)
                        {
                            Player P = ctDay.GetPlayer(ctName);
                            if (P == null)
                                continue;
                            playingBall b = ctDay.getMyBall(P.name);
                            if (b == null)
                                continue;
                            string ballMate = b.GetBallMate(P.name);
                            if (!string.IsNullOrWhiteSpace(ballMate))
                                ballMate = string.Format("({0})", ballMate);
                            cnt++;
                            AddDetailCell((object)ctPdfPTable, P.name + ballMate, isEvenLine);

                            AddDetailCell((object)ctPdfPTable, P.initialHcp, isEvenLine, OutputFormat.Number2);
                            AddDetailCell((object)ctPdfPTable, b.GetPlayingHcp(), isEvenLine, OutputFormat.Number2);
                            for (int i = 1; i <= 9; i++)
                            {
                                String s = String.Format("{0}|{1}", ctDay.scores.getScore(P.name, i) < 1 ? "X" : ctDay.scores.getScore(P.name, i).ToString(), ctDay.stblPoints.getStblPointsForHole(P.name, i));
                                AddDetailCell((object)ctPdfPTable, s, isEvenLine, OutputFormat.Number);
                            }
                            AddTotalCell((object)ctPdfPTable, ctDay.stblPoints.getStblPointsForLastHoles(P.name, 18) - ctDay.stblPoints.getStblPointsForLastHoles(P.name, 9), isEvenLine, OutputFormat.Number);
                            for (int i = 10; i <= 18; i++)
                            {
                                String s = String.Format("{0}|{1}", ctDay.scores.getScore(P.name, i) < 1 ? "X" : ctDay.scores.getScore(P.name, i).ToString(), ctDay.stblPoints.getStblPointsForHole(P.name, i));
                                AddDetailCell((object)ctPdfPTable, s, isEvenLine, OutputFormat.Number);
                            }

                            AddTotalCell((object)ctPdfPTable, ctDay.stblPoints.getStblPointsForLastHoles(P.name, 9), isEvenLine, OutputFormat.Number);
                            AddDetailCell((object)ctPdfPTable, ctDay.stblPoints.getStblPointsForLastHoles(P.name, 6), isEvenLine, OutputFormat.Number);
                            AddDetailCell((object)ctPdfPTable, ctDay.stblPoints.getStblPointsForLastHoles(P.name, 3), isEvenLine, OutputFormat.Number);
                            AddTotalCell((object)ctPdfPTable, ctDay.stblPoints.getStblPointsForLastHoles(P.name, 18), isEvenLine, OutputFormat.Number);
                            AddDetailCell((object)ctPdfPTable, string.Format("#{0}", cnt), isEvenLine, OutputFormat.Number);

                            isEvenLine = !isEvenLine;
                        }
                    }
                    else
                        ctPdfPTable.AddCell(GetTitleCellAlternative(ctPdfPTable.NumberOfColumns, "Foursome, no stbl!"));


                    #endregion
                    //EmptyRow
                    ctPdfPTable.AddCell(GetEmptyRow(ctPdfPTable.NumberOfColumns));
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);

                #endregion

                #endregion
                #endregion
                document.NewPage(); ++pageN;
                #region Page Teams
                CreatePDFHeaderAndFooter(document, null, "Flights, Teams and Hcps", "", contentbyte, pageN);

                colWidth_1st = 220;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_1st;
                ctX = document.Left;
                ctY = document.Top - topAfterHeader;
                #region 1st Column Teams

                ctPdfPTable = new PdfPTable(new float[] { 3f, 2f, 4f, 2f, 2f, 2f, 2f, 2f, 2f });
                ctPdfPTable.TotalWidth = ctColWidth;
                foreach (day ctDay in c.days)
                {
                    if (ctDay.courseDefinition == null)
                        break;
                    isEvenLine = false;
                    //Title
                    ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Rnd {0} ({7}): {1} ({4}, par:{6} cr:{2}, sr:{3}, Dist: {5})", new object[] { ctDay.nr, ctDay.courseDefinition.name, ctDay.courseDefinition.cr, ctDay.courseDefinition.sr, ctDay.courseDefinition.TeeColor, ctDay.courseDefinition.Yards, ctDay.courseDefinition.getSumPar(), ctDay.playModeDisplay })));
                    #region Table Column Headers
                    AddHeaderCell((object)ctPdfPTable, "Mode");
                    AddHeaderCell((object)ctPdfPTable, "T");
                    AddHeaderCell((object)ctPdfPTable, "Player");
                    AddHeaderCell((object)ctPdfPTable, "Hcp", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "Play", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "c4B", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "1/3", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "cFS", OutputFormat.TextRight);
                    AddHeaderCell((object)ctPdfPTable, "2/3", OutputFormat.TextRight);
                    #endregion
                    #region DetailsCells
                    int cnt = -1;
                    foreach (flight ctFlight in ctDay.flights.Values)
                    {
                        foreach (team ctTeam in ctFlight.teams.Values)
                        {
                            foreach (playingBall b in ctTeam.playingBalls.Values)
                            {
                                foreach (Player ctPlayer in b.players.Values)
                                {
                                    string ballName = "";
                                    if (ctFlight.matchType == flight.MatchType.Match4b5 && b.nbOfPlayerForBall > 1)
                                        ballName = b.name;

                                    AddDetailCell((object)ctPdfPTable, ctFlight.matchType.ToString(), isEvenLine);
                                    AddDetailCell((object)ctPdfPTable, ctFlight.name + ctTeam.name1based + ballName, isEvenLine);
                                    AddDetailCell((object)ctPdfPTable, ctPlayer.name, isEvenLine);
                                    AddDetailCell((object)ctPdfPTable, ctPlayer.initialHcp, isEvenLine, OutputFormat.Number2);
                                    AddDetailCell((object)ctPdfPTable, ctPlayer.playingHcp, isEvenLine, OutputFormat.Number2);

                                    if (ctFlight.coupsRecu4B[ctTeam.name + ctPlayer.name] > 0)
                                    {
                                        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecu4B[ctTeam.name + ctPlayer.name], isEvenLine, OutputFormat.Number2);
                                        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecu4B[ctTeam.name + ctPlayer.name]/3, isEvenLine, OutputFormat.Number2);
                                    }
                                    else
                                    {
                                        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                    }

                                    if (ctFlight.coupsRecuFS[ctTeam.name] > 0)
                                    {
                                        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecuFS[ctTeam.name], isEvenLine, OutputFormat.Number2);
                                        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecuFS[ctTeam.name]*2/3, isEvenLine, OutputFormat.Number2);
                                    }
                                    else
                                    {
                                        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                    }
                                    //if (ctDay.isFoursome && ctFlight.matchType != flight.MatchType.Match2b)
                                    //{
                                    //    if (ctFlight.coupsRecuFS[ctTeam.name] > 0)
                                    //        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecuFS[ctTeam.name], isEvenLine, OutputFormat.Number2);
                                    //    else
                                    //        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                    //}
                                    //else
                                    //{
                                    //    if (ctFlight.coupsRecu4B[ctTeam.name + b.name] > 0)
                                    //        AddDetailCell((object)ctPdfPTable, ctFlight.coupsRecu4B[ctTeam.name + b.name], isEvenLine, OutputFormat.Number2);
                                    //    else
                                    //        AddDetailCell((object)ctPdfPTable, "-", isEvenLine, OutputFormat.TextRight);
                                    //}
                                }
                            }
                        }
                        isEvenLine = !isEvenLine;
                    }

                    
                    #endregion
                    //EmptyRow
                    ctPdfPTable.AddCell(GetEmptyRow(ctPdfPTable.NumberOfColumns));
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);

                #endregion
                ctColWidth = colWidth_2nd;
                ctX = document.Left + colWidth_1st + interstice;
                ctY = document.Top - topAfterHeader;
                #region 2nd Column xTimes, stats
                #region Table stats
                ctPdfPTable = new PdfPTable(new float[] { 4f, 2f, 2f, 2f, 2f });
                ctPdfPTable.TotalWidth = ctColWidth / 2;

                ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Flight distribution, how many times..")));

                AddHeaderCell((object)ctPdfPTable, " ");
                AddHeaderCell((object)ctPdfPTable, "0x", OutputFormat.TextRight);
                AddHeaderCell((object)ctPdfPTable, "1x", OutputFormat.TextRight);
                AddHeaderCell((object)ctPdfPTable, "2x", OutputFormat.TextRight);
                AddHeaderCell((object)ctPdfPTable, "3x", OutputFormat.TextRight);

                AddHeaderCell((object)ctPdfPTable, "..sameFlight", OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTFlight[0].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTFlight[1].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTFlight[2].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTFlight[3].ToString(), true, OutputFormat.TextRight);

                AddHeaderCell((object)ctPdfPTable, "..sameTeam", OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTTeam[0].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTTeam[1].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTTeam[2].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTTeam[3].ToString(), true, OutputFormat.TextRight);

                AddHeaderCell((object)ctPdfPTable, "..asEnemy", OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTEnemy[0].ToString(), true, OutputFormat.TextRight, null, ColorLightBlue);
                AddDetailCell((object)ctPdfPTable, c.statXTEnemy[1].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTEnemy[2].ToString(), true, OutputFormat.TextRight, null, ColorGrey2);
                AddDetailCell((object)ctPdfPTable, c.statXTEnemy[3].ToString(), true, OutputFormat.TextRight);

                AddHeaderCell((object)ctPdfPTable, "..in2B", OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTIn2B[0].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTIn2B[1].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTIn2B[2].ToString(), true, OutputFormat.TextRight);
                AddDetailCell((object)ctPdfPTable, c.statXTIn2B[3].ToString(), true, OutputFormat.TextRight);

                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX + (ctColWidth / 2), ctY, contentbyte);
                #endregion
                ctY -= ctPdfPTable.TotalHeight + interstice;
                #region Table xTimes
                switch (nbOfPlayer)
                {
                    case 11:
                        ctPdfPTable = new PdfPTable(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                        break;
                    case 10:
                        ctPdfPTable = new PdfPTable(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                        break;
                    case 9:
                        ctPdfPTable = new PdfPTable(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                        break;
                    case 8:
                        ctPdfPTable = new PdfPTable(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                        break;
                    case 7:
                        ctPdfPTable = new PdfPTable(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f });
                        break;
                }
                ctPdfPTable.TotalWidth = ctColWidth;

                ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("How many times inSameFlight|inSameTeam|asEnemy")));

                AddHeaderCell((object)ctPdfPTable, " ");
                foreach (Player P in c.Players)
                {
                    AddHeaderCell((object)ctPdfPTable, P.name.Left(4));
                }

                foreach (Player P2 in c.Players)
                {
                    AddHeaderCell((object)ctPdfPTable, P2.name.Left(4));
                    foreach (Player P1 in c.Players)
                    {
                        if (P1.name == P2.name)
                        {
                            AddDetailCell((object)ctPdfPTable, "-", isEvenLine);
                        }
                        else
                        {
                            int ctHTFlight = c.GetHowManyTimeInSameFlight(P1, P2);
                            int ctHTTeam = c.GetHowManyTimeInSameTeam(P1, P2);
                            int ctHTEnemy = c.GetHowManyTimeEnemy(P1, P2);
                            
                            sText = String.Format("{0} | {1} | {2}", ctHTFlight, ctHTTeam, ctHTEnemy);
                            if (ctHTEnemy == 0)
                                AddDetailCell((object)ctPdfPTable, sText, true, OutputFormat.Text, null, ColorLightBlue); 
                            else if(ctHTEnemy == 2)
                                AddDetailCell((object)ctPdfPTable, sText, true, OutputFormat.Text, null, ColorGrey2);
                            else
                                AddDetailCell((object)ctPdfPTable, sText, true); 
                        }
                    }
                }
                
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);
                #endregion
                #endregion
                #endregion
                #endregion
                document.NewPage(); ++pageN;
                #region page newHcps
                CreatePDFHeaderAndFooter(document, null, "New Hcps for Next Year", "", contentbyte, pageN);

                colWidth_1st = 380;
                colWidth_2nd = document.Right - document.Left - colWidth_1st - interstice;

                #region 1st Row
                ctColWidth = colWidth_1st;
                ctX = document.Left;
                ctY = document.Top - topAfterHeader;
                #region 1st newHcps

                switch (nbOfPlayer)
                {
                    case 11:
                        ctPdfPTable = new PdfPTable(new float[] { 10f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 10:
                        ctPdfPTable = new PdfPTable(new float[] { 10f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 9:
                        ctPdfPTable = new PdfPTable(new float[] { 10f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 8:
                        ctPdfPTable = new PdfPTable(new float[] { 10f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                    case 7:
                        ctPdfPTable = new PdfPTable(new float[] { 10f, 4f, 4f, 4f, 4f, 4f, 4f, 4f });
                        break;
                }
                ctPdfPTable.TotalWidth = ctColWidth;

                AddHeaderCell((object)ctPdfPTable, " ");
                foreach (Player P in c.Players)
                {
                    AddHeaderCell((object)ctPdfPTable, P.name.Left(4));
                }
                AddDetailCell((object)ctPdfPTable, "initial Hcp", isEvenLine);
                foreach (Player P in c.Players)
                {
                    AddDetailCell((object)ctPdfPTable, P.initialHcp, isEvenLine, OutputFormat.Number1);
                }
                
                foreach (day ctDay in c.days)
                {
                    if (ctDay.courseDefinition == null)
                        break;
                    isEvenLine = true;
                    //Title
                    ctPdfPTable.AddCell(GetTitleCell(ctPdfPTable.NumberOfColumns, String.Format("Rnd {0} ({7}): {1} ({4}, par:{6} cr:{2}, sr:{3}, Dist: {5})", new object[] { ctDay.nr, ctDay.courseDefinition.name, ctDay.courseDefinition.cr, ctDay.courseDefinition.sr, ctDay.courseDefinition.TeeColor, ctDay.courseDefinition.Yards, ctDay.courseDefinition.getSumPar(), ctDay.playModeDisplay })));

                    if (ctDay.stblPoints.isValidForStblDay())
                    {
                        isEvenLine = !isEvenLine;
                        AddDetailCell((object)ctPdfPTable, "Pts Stable Jour ASG", isEvenLine);
                        foreach (Player P in c.Players)
                        {
                            AddDetailCell((object)ctPdfPTable, ctDay.stblPointsForNewHcp.getStblPointsForLastHoles(P.name), isEvenLine, OutputFormat.Number);
                        }
                        isEvenLine = !isEvenLine;
                        AddDetailCell((object)ctPdfPTable, "Hcp ASG", isEvenLine);
                        foreach (Player P in c.Players)
                        {
                            AddDetailCell((object)ctPdfPTable, ctDay.NewHcps.hpcs[P.name], isEvenLine, OutputFormat.Number1);
                        }
                    }
                }
                ctPdfPTable.WriteSelectedRows(0, ctPdfPTable.Rows.Count, ctX, ctY, contentbyte);

                #endregion

                #endregion
                #endregion
            }
            finally
            {
                document.Close();
            }
        }
        void CreatePDFHeaderAndFooter(Document document, Dictionary<String, String> ImageNames, string PageTitle, string PageSubTitle, PdfContentByte contentByte, int pageN)
        {
            #region Page Header + Logo
            #region Header
            // main header with reference date
            ColumnText headColumn = new ColumnText(contentByte);
            headColumn.SetSimpleColumn(document.Left, document.Top - 30, document.Right, document.Top);
            Chunk heading = new Chunk(PageTitle, fontDocHeader);
            headColumn.AddText(heading);
            headColumn.Go();
            // subheader with fund name
            headColumn.SetSimpleColumn(document.Left, document.Top - 80, document.Right, document.Top - 19);
            heading = new Chunk(PageSubTitle, fontDocSubHeader);
            headColumn.AddText(heading);
            headColumn.Go();
            #endregion


            #region Logo

            iTextSharp.text.Image ctGraph;
            float ctImageHeight = 30.15f;
            float ctImageWidth, origImageHeight, origImageWidth, firstWidth;
            float interstice = 20f;

            ctGraph = iTextSharp.text.Image.GetInstance(Path.Combine("Assets", "image.jpg"));
            origImageWidth = ctGraph.Width;
            origImageHeight = ctGraph.Height;
            firstWidth = ctImageWidth = ctImageHeight;
            if (origImageHeight != 0)
                firstWidth = ctImageWidth = origImageWidth * ctImageHeight / origImageHeight;
            ctGraph.ScaleAbsolute(ctImageWidth, ctImageHeight);
            ctGraph.SetAbsolutePosition(document.Right - ctImageWidth, document.Top - ctImageHeight);
            document.Add(ctGraph);

            #endregion
            #endregion
            #region Footer
            int textFooterSize = 6;
            float textFooterLen = 10;
            String footertext = "Shamrock";
            String pagetext = "";
            float pagetextLen = 10;
            float ctFooterX = 0;
            float ctFooterY = 0;

            #region footer
            ctFooterX = document.Left;
            ctFooterY = document.Bottom - 10;
            textFooterLen = basefont.GetWidthPoint(footertext, textFooterSize);
            contentByte.BeginText();
            contentByte.SetFontAndSize(basefont, textFooterSize);
            contentByte.SetTextMatrix(ctFooterX, ctFooterY);
            contentByte.ShowText(footertext);
            contentByte.EndText();
            pagetext = pageN.ToString(); // "Page " + pageN.ToString();
            pagetextLen = basefont.GetWidthPoint(pagetext, textFooterSize);
            ctFooterX = document.Right - pagetextLen;
            contentByte.BeginText();
            contentByte.SetFontAndSize(basefont, textFooterSize);
            contentByte.SetTextMatrix(ctFooterX, ctFooterY);
            contentByte.ShowText(pagetext);
            contentByte.EndText();
            #endregion

            #endregion
        }

        PdfPCell GetEmptyRow(Int32 span)
        {
            PdfPCell ret = new PdfPCell();

            ret.Phrase = new Phrase(" ", fontTableTitle);
            ret.BackgroundColor = BaseColor.WHITE;
            ret.BorderColor = new BaseColor(System.Drawing.Color.White);
            ret.BorderWidth = 0.3f;
            ret.FixedHeight = 2; //Header-Height
            ret.Colspan = span;
            ret.HorizontalAlignment = 0;

            return ret;
        }
        PdfPCell GetTitleCell(Int32 span, String cellContent)
        {
            PdfPCell ret = new PdfPCell();

            ret.Phrase = new Phrase(cellContent, fontTableTitle);
            ret.BackgroundColor = ColorLightBlue;
            ret.BorderColor = new BaseColor(System.Drawing.Color.White);
            ret.BorderWidth = 0.3f;
            //ret.FixedHeight = 10; //Header-Height
            ret.Colspan = span;
            ret.HorizontalAlignment = 0;

            return ret;
        }
        PdfPCell GetTitleCellAlternative(Int32 span, String cellContent)
        {
            PdfPCell ret = new PdfPCell();

            ret.Phrase = new Phrase(cellContent, fontTableTitleAlternative);
            ret.BackgroundColor = ColorGrey4;
            ret.BorderColor = new BaseColor(System.Drawing.Color.White);
            ret.BorderWidth = 0.3f;
            //ret.FixedHeight = 10; // Header-Height
            ret.Colspan = span;
            ret.HorizontalAlignment = 0;

            return ret;
        }

        void AddHeaderCell(object destination, String cellContent, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null)
        {
            if (destination is PdfPTable)
            {
                ((PdfPTable)destination).AddCell(GetHeaderCell(cellContent, format, customfont, BackGroundColor));
            }
        }
        PdfPCell GetHeaderCell(String cellContent, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null)
        {
            PdfPCell ret = new PdfPCell();

            if (customfont != null)
                ret.Phrase = new Phrase(cellContent, customfont);
            else
                ret.Phrase = new Phrase(cellContent, fontTableHeader);

            if (BackGroundColor != null)
                ret.BackgroundColor = BackGroundColor;
            else
                ret.BackgroundColor = ColorGrey2;
            ret.BorderColor = new BaseColor(System.Drawing.Color.White);
            ret.BorderWidth = 0.3f;

            switch (format)
            {
                case OutputFormat.Number:
                case OutputFormat.Number1:
                case OutputFormat.Number2:
                case OutputFormat.Number4:
                case OutputFormat.Percent:
                case OutputFormat.TextRight:
                    ret.HorizontalAlignment = 2; // Right
                    break;
                case OutputFormat.TextMid:
                    ret.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED_ALL; // Mid
                    break;
                default:
                    ret.HorizontalAlignment = 0; // Left
                    break;
            }

            return ret;
        }

        void AddDetailCell(object destination, DateTime cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Date, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null , bool separator = false)
        {
            if (destination is PdfPTable)
            {
                if (format == OutputFormat.Date)
                    ((PdfPTable)destination).AddCell(GetDetailCell(cellContent.ToShortDateString(), isEvenLine, format, customfont, BackGroundColor, separator));
                else
                    ((PdfPTable)destination).AddCell(GetDetailCell(cellContent.ToString(), isEvenLine, format, customfont, BackGroundColor, separator));
            }
        }
        void AddDetailCell(object destination, Double cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Number, Boolean UseNegColor = true, bool separator = false, bool isLarge = false)
        {
            if (destination is PdfPTable)
            {
                ((PdfPTable)destination).AddCell(GetDetailCell(cellContent, isEvenLine, format, UseNegColor, separator, isLarge));
            }
        }
        void AddDetailCell(object destination, String cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null, bool separator = false)
        {
            if (destination is PdfPTable)
            {
                ((PdfPTable)destination).AddCell(GetDetailCell(cellContent, isEvenLine, format, customfont, BackGroundColor, separator));
            }
        }
        void AddTotalCell(object destination, Double cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Number, Boolean UseNegColor = true, bool separator = false, bool isLarge = false)
        {
            if (destination is PdfPTable)
            {
                ((PdfPTable)destination).AddCell(GetTotalCell(cellContent, isEvenLine, format, false));
            }
        }
        void AddTotalCell(object destination, String cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null, bool separator = false)
        {
            if (destination is PdfPTable)
            {
                ((PdfPTable)destination).AddCell(GetTotalCell(cellContent, isEvenLine, format, customfont));
            }
        }

        PdfPCell GetDetailCell(Double cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Number, Boolean UseNegColor = true, bool separator = false, bool isLarge = false)
        {
            String cellAsString;
            if (cellContent == 0)
                cellAsString = "-";
            else
            {
                switch (format)
                {
                    case OutputFormat.Number:
                        cellAsString = cellContent.ToString("N0", clientNumberFormat);
                        break;
                    case OutputFormat.Number1:
                        cellAsString = cellContent.ToString("N1", clientNumberFormat);
                        break;
                    case OutputFormat.Number2:
                        cellAsString = cellContent.ToString("N2", clientNumberFormat);
                        break;
                    case OutputFormat.Number4:
                        cellAsString = cellContent.ToString("N4", clientNumberFormat);
                        break;
                    case OutputFormat.Percent:
                    case OutputFormat.PercentLeft:
                        cellAsString = cellContent.ToString("P2", clientNumberFormat);
                        break;
                    default:
                        cellAsString = cellContent.ToString(clientNumberFormat);
                        break;
                }
            }
            if (UseNegColor && cellContent < 0)
                return GetDetailCell(cellAsString, isEvenLine, format, fontDetailNeg, separator: separator);

            return GetDetailCell(cellAsString, isEvenLine, format, separator: separator);

        }
        PdfPCell GetDetailCell(String cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null, BaseColor BackGroundColor = null, bool separator = false)
        {
            PdfPCell ret = new PdfPCell();
            if (customfont != null)
                ret.Phrase = new Phrase(cellContent, customfont);
            else
                ret.Phrase = new Phrase(cellContent, fontDetail);

            if (BackGroundColor != null)
                ret.BackgroundColor = BackGroundColor;
            else if (isEvenLine)
                ret.BackgroundColor = ColorGrey4;
            else
                ret.BackgroundColor = ColorGrey5;

            ret.BorderColor = new BaseColor(System.Drawing.Color.White);
            ret.BorderWidth = 0.3f;
            //ret.FixedHeight = 10;

            switch (format)
            {
                case OutputFormat.Number:
                case OutputFormat.Number1:
                case OutputFormat.Number2:
                case OutputFormat.Number4:
                case OutputFormat.Percent:
                case OutputFormat.TextRight:
                    ret.HorizontalAlignment = 2; // Right
                    break;
                case OutputFormat.TextMid:
                    ret.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED_ALL; // Mid
                    break;
                case OutputFormat.PercentLeft:
                    ret.HorizontalAlignment = 0;
                    break;
                default:
                    ret.HorizontalAlignment = 0; // Left
                    break;
            }

            if (separator)
            {
                ret.BorderColorBottom = new BaseColor(System.Drawing.Color.Tomato);
                ret.BorderWidthBottom = 1;
            }

            return ret;
        }
        PdfPCell GetTotalCell(Double cellContent, Boolean isEvenLine = true, OutputFormat format = OutputFormat.Number, Boolean UseNegColor = true)
        {
            String cellAsString;

            switch (format)
            {
                case OutputFormat.Number:
                    cellAsString = cellContent.ToString("N0", clientNumberFormat);
                    break;
                case OutputFormat.Number1:
                    cellAsString = cellContent.ToString("N1", clientNumberFormat);
                    break;
                case OutputFormat.Number2:
                    cellAsString = cellContent.ToString("N2", clientNumberFormat);
                    break;
                case OutputFormat.Number4:
                    cellAsString = cellContent.ToString("N4", clientNumberFormat);
                    break;
                case OutputFormat.Percent:
                case OutputFormat.PercentLeft:
                    cellAsString = cellContent.ToString("P2", clientNumberFormat);
                    break;
                default:
                    cellAsString = cellContent.ToString(clientNumberFormat);
                    break;
            }

            if (UseNegColor && cellContent < 0)
                return GetTotalCell(cellAsString, isEvenLine, format, fontDetailNeg);

            return GetTotalCell(cellAsString, isEvenLine, format);
        }
        PdfPCell GetTotalCell(String cellContent, Boolean isEvenLine, OutputFormat format = OutputFormat.Text, iTextSharp.text.Font customfont = null)
        {
            PdfPCell ret = new PdfPCell();
            if (customfont != null)
                ret.Phrase = new Phrase(cellContent, customfont);
            else
                ret.Phrase = new Phrase(cellContent, fontTotal);

            //if (isEvenLine)
            //    ret.BackgroundColor = ColorGrey4;
            //else
            //    ret.BackgroundColor = ColorGrey5;
            ret.BackgroundColor = ColorLightBlue;

            ret.BorderColor = BaseColor.WHITE;
            ret.BorderWidth = 0.3f;
            //ret.FixedHeight = 10; // Header-Height

            //ret.BorderColorTop = BaseColor.BLACK;
            //ret.BorderWidthTop = 1;

            switch (format)
            {
                case OutputFormat.Number:
                case OutputFormat.Number1:
                case OutputFormat.Number2:
                case OutputFormat.Number4:
                case OutputFormat.Percent:
                case OutputFormat.TextRight:
                    ret.HorizontalAlignment = 2; // Right
                    break;
                case OutputFormat.TextMid:
                    ret.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED_ALL; // Mid
                    break;
                case OutputFormat.PercentLeft:
                    ret.HorizontalAlignment = 0;
                    break;
                default:
                    ret.HorizontalAlignment = 0; // Left
                    break;
            }
            return ret;
        }
    }
}
