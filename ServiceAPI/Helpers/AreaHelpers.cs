using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Helpers
{
    public enum Areas  {Carparks,Airports, Bookings, Pricings,Slots, Users};
    public enum Operation { Edit, Create, Delete, Update}
    public static class AreaHelpers
    {
        public static IHtmlString AreaHeader(this HtmlHelper helper, Areas area, Operation operation, string text="")
        {
            string icon = string.Empty;
            switch (area)
            {
                case Areas.Airports: icon = "fa-send"; break;
                case Areas.Carparks: icon = "fa-automobile"; break;
                case Areas.Bookings: icon = "fa-barcode"; break;
                case Areas.Slots: icon = "fa-calendar"; break;
                case Areas.Pricings: icon = "fa-table"; break;
                case Areas.Users: icon = "fa-barcode"; break;
            }

            string htmlString = String.Format(
                @"<div class='well'>
                        <div class='row'>
                            <div class='col-md-0'>
                            </div>
                            <div class='col-md-8'>
                                <h2><span class='fa fa-1x fa-fw {0} text-success'></span>{1} {2}<small>{3}</small></h2>
                            </div>
                            <div class='col-md-4'>
                            </div>
                        </div>
                    </div>",icon, operation.ToString(), area.ToString(), text);
            return new HtmlString(htmlString);
        }

        public static IHtmlString AreaSubmit(this HtmlHelper helper, Areas area, Operation operation)
        {
            string htmlString = String.Format(
             @"<div class='well'>
                    <div class='row'>
                        <div class='col-md-6'>
                            <a class='btn btn-default' href='/{0}Admin'>Back</a>
                            <button class='btn btn-warning' type='submit'>Clear Fields</button>
                            <button class='btn btn-primary' type='submit'>{1}</button>
                        </div>
                    </div>               </div>                ", area.ToString().Remove(area.ToString().Length-1), operation.ToString());
            return new HtmlString(htmlString);
        }
    }

}