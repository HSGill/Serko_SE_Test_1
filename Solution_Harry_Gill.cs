/**
 * AUTHOR : Harry Gill
 * CREATED : 02-04-2019
*  Description: REST SERVICE to return GST and Total Expense
 * Programming Language : C#
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;

namespace Serko_REST_Service.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        public XElement Get(string text)
        {
            List<string> matches = new List<string>();
            Regex regex = new Regex("<([^<>]+).*>.*</\\1>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match match = regex.Match(text);                 //<([^>]+).*>.*</\\1>, Regular Expression looks for < followed by  > 
            while (match.Success)                            // matches exactly the previous element
            {
                matches.Add(match.Value);
                match = match.NextMatch();
             }
            if (!matches.Contains("total"))
            {
                throw new Exception("Please add total expense XML ");
            }
            
            XElement embedded_xml = new XElement("r", matches.Select(str => new XElement("r", str))); //Converting List to XML
            
            return embedded_xml;
        }
        /// <summary>
        /// Function calculating Total expense(without GST) and GST
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>total expense excluding GST</returns>
        public float Total_Expense(XmlDocument doc)
        {
            XmlNodeList xnList = doc.SelectNodes("/r/r");
            float total_expense = 0;
            foreach (XmlNode xn in xnList)
            {
                total_expense = float.Parse(xn["total"].InnerText);        

            }
            float total_expense_excludingGST = (total_expense * 100) / 115;
            float GST = total_expense - total_expense_excludingGST;
            return (total_expense_excludingGST);
        }
    }
}
