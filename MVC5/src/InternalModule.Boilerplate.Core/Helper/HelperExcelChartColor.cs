using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace InternalModule.Boilerplate.Core.Helper
{
    public static class ChartColor
    {
        public static void SetChartPointRandomColors(this ExcelChart chart, int serieNumber)
        {
            var chartXml = chart.ChartXml;

            var nsa = chart.WorkSheet.Drawings.NameSpaceManager.LookupNamespace("a");
            var nsuri = chartXml.DocumentElement.NamespaceURI;

            var nsm = new XmlNamespaceManager(chartXml.NameTable);
            nsm.AddNamespace("a", nsa);
            nsm.AddNamespace("c", nsuri);

            var serieNode = chart.ChartXml.SelectSingleNode(@"c:chartSpace/c:chart/c:plotArea/c:barChart/c:ser[c:idx[@val='" + serieNumber + "']]", nsm);
            var serie = chart.Series[serieNumber];
            var points = serie.Series.Length;
            var rand = new Random(serieNumber);

            for (var i = 1; i <= points; i++)
            {
                var dPt = chartXml.CreateNode(XmlNodeType.Element, "dPt", nsuri);
                var idx = chartXml.CreateNode(XmlNodeType.Element, "idx", nsuri);
                var att = chartXml.CreateAttribute("val", nsuri);
                att.Value = i.ToString();
                idx.Attributes.Append(att);
                dPt.AppendChild(idx);

                var srgbClr = chartXml.CreateNode(XmlNodeType.Element, "srgbClr", nsa);
                att = chartXml.CreateAttribute("val");

                //Generate a random color - override with own logic to specify
                var color = ColorTranslator.FromHtml("#FFBE7D");
                att.Value = $"{color.R:X2}{color.G:X2}{color.B:X2}";
                srgbClr.Attributes.Append(att);

                var solidFill = chartXml.CreateNode(XmlNodeType.Element, "solidFill", nsa);
                solidFill.AppendChild(srgbClr);

                var spPr = chartXml.CreateNode(XmlNodeType.Element, "spPr", nsuri);
                spPr.AppendChild(solidFill);

                dPt.AppendChild(spPr);
                serieNode.AppendChild(dPt);
            }
        }

        public static void SetDataPointStyle(this ExcelChart chart, ExcelChartSerie series, string color, string borderColor = "000000")
        {
            var i = 0;
            var found = false;
            foreach (var s in chart.Series)
            {
                if (s == series)
                {
                    found = true;
                    break;
                }
                ++i;
            }
            if (!found) throw new InvalidOperationException("series not found.");

            var nsm = chart.WorkSheet.Drawings.NameSpaceManager;
            var nschart = nsm.LookupNamespace("c");
            var nsa = nsm.LookupNamespace("a");
            var node = chart.ChartXml.SelectSingleNode(@"c:chartSpace/c:chart/c:plotArea/c:barChart/c:ser[c:idx[@val='" + i.ToString(
                CultureInfo.InvariantCulture) + "']]", nsm);
            var doc = chart.ChartXml;

            var spPr = doc.CreateElement("c:spPr", nschart);
            var solidFill = spPr.AppendChild(doc.CreateElement("a:solidFill", nsa));
            var srgbClr = solidFill.AppendChild(doc.CreateElement("a:srgbClr", nsa));
            var valattrib = srgbClr.Attributes.Append(doc.CreateAttribute("val"));
            valattrib.Value = color;

            var ln = spPr.AppendChild(doc.CreateElement("a:ln", nsa));
            var lnSolidFill = ln.AppendChild(doc.CreateElement("a:solidFill", nsa));
            var lnSrgbClr = lnSolidFill.AppendChild(doc.CreateElement("a:srgbClr", nsa));
            var lnValattrib = lnSrgbClr.Attributes.Append(doc.CreateAttribute("val"));
            lnValattrib.Value = borderColor;

            node.AppendChild(spPr);
        }

        public static void SetSeriesStyle(this ExcelChart chart, ExcelChartSerie series, Color color, decimal? thickness = null)
        {
            if (thickness < 0) throw new ArgumentOutOfRangeException("thickness");
            var i = 0;
            var found = false;
            foreach (var s in chart.Series)
            {
                if (s == series)
                {
                    found = true;
                    break;
                }
                ++i;
            }
            if (!found) throw new InvalidOperationException("series not found.");
            //Get the nodes
            var nsm = chart.WorkSheet.Drawings.NameSpaceManager;
            var nschart = nsm.LookupNamespace("c");
            var nsa = nsm.LookupNamespace("a");
            var node = chart.ChartXml.SelectSingleNode(@"c:chartSpace/c:chart/c:plotArea/c:lineChart/c:ser[c:idx[@val='" + i.ToString(CultureInfo.InvariantCulture) + "']]", nsm);
            var doc = chart.ChartXml;

            //Add the solid fill node
            var spPr = doc.CreateElement("c:spPr", nschart);
            var ln = spPr.AppendChild(doc.CreateElement("a:ln", nsa));
            if (thickness.HasValue)
            {
                var w = ln.Attributes.Append(doc.CreateAttribute("w"));
                w.Value = Math.Round(thickness.Value * 12700).ToString(CultureInfo.InvariantCulture);
                var cap = ln.Attributes.Append(doc.CreateAttribute("cap"));
                cap.Value = "rnd";
            }
            var solidFill = ln.AppendChild(doc.CreateElement("a:solidFill", nsa));
            var srgbClr = solidFill.AppendChild(doc.CreateElement("a:srgbClr", nsa));
            var valattrib = srgbClr.Attributes.Append(doc.CreateAttribute("val"));

            //Set the color
            valattrib.Value = $"{color.R:X2}{color.G:X2}{color.B:X2}"; ;
            node.AppendChild(spPr);
        }

        public static void SetLineChartColor(this ExcelChart chart, int serieIdx, int chartSeriesIndex, string color)
        {
            var chartXml = chart.ChartXml;

            var nsa = chart.WorkSheet.Drawings.NameSpaceManager.LookupNamespace("a");
            var nsuri = chartXml.DocumentElement.NamespaceURI;

            var nsm = new XmlNamespaceManager(chartXml.NameTable);
            nsm.AddNamespace("a", nsa);
            nsm.AddNamespace("c", nsuri);

            var serieNode = chart.ChartXml.SelectSingleNode($@"c:chartSpace/c:chart/c:plotArea/c:lineChart/c:ser[c:idx[@val='{serieIdx}']]", nsm);
            var serie = chart.Series[chartSeriesIndex];
            var points = serie.Series.Length;

            //Add reference to the color for the legend
            var srgbClr = chartXml.CreateNode(XmlNodeType.Element, "srgbClr", nsa);
            var att = chartXml.CreateAttribute("val");
            att.Value = color;
            srgbClr.Attributes.Append(att);

            var solidFill = chartXml.CreateNode(XmlNodeType.Element, "solidFill", nsa);
            solidFill.AppendChild(srgbClr);

            var ln = chartXml.CreateNode(XmlNodeType.Element, "ln", nsa);
            ln.AppendChild(solidFill);

            var spPr = chartXml.CreateNode(XmlNodeType.Element, "spPr", nsuri);
            spPr.AppendChild(ln);

            serieNode.AppendChild(spPr);
        }

        public static void SetSeriesLineMarkerColor(this ExcelChart chart, int serieIdx, string color,
            int makrkerSize = 7, eMarkerStyle markerStyle = eMarkerStyle.Circle)
        {
            ExcelScatterChartSerie serie = (ExcelScatterChartSerie)chart.Series[serieIdx];
            serie.LineColor = ColorTranslator.FromHtml(color);
            serie.MarkerColor = ColorTranslator.FromHtml(color);
            serie.MarkerLineColor = ColorTranslator.FromHtml(color);
            serie.MarkerSize = makrkerSize;
            serie.Marker = markerStyle;
        }

        public static void SetSeriesLineColor(this ExcelChart chart, int serieIdx, string color,
            double lineWidth = 2.25, eLineStyle lineStyle = eLineStyle.Solid)
        {
            ExcelLineChartSerie serie = (ExcelLineChartSerie)chart.Series[serieIdx];
            serie.LineColor = ColorTranslator.FromHtml(color);
            serie.LineWidth = lineWidth;
            serie.Border.LineStyle = lineStyle;
        }

        public static String ToHex(this Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
