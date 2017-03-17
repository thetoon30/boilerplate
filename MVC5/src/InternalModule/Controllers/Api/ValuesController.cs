using InternalModule.Boilerplate.Models;
using InternalModule.Boilerplate.Models.ResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace InternalModule.Boilerplate.Controllers.Api
{
    /// <summary>
    /// Example Web API : ValuesController need authorized by attribute [Authorize]
    /// also can change alias name by [RoutePrefix("xName")]
    /// For more information on Model Validation using Data Annotation attributes, see the MSDN Documentation at http://go.microsoft.com/fwlink/?LinkId=159063
    /// Required – Indicates that the property is a required field
    /// DisplayName – Defines the text we want used on form fields and validation messages
    /// StringLength – Defines a maximum length for a string field
    /// Range – Gives a maximum and minimum value for a numeric field
    /// Bind – Lists fields to exclude or include when binding parameter or form values to model properties
    /// ScaffoldColumn – Allows hiding fields from editor forms
    /// </summary>
    [Authorize]
    [RoutePrefix("Value")]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Get Simple Value - return String
        /// </summary>
        /// <returns></returns>
        [Route("GetSimpleValue")]
        public string Get()
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return String.Format("Hello, {0}.", userName);
        }

        /// <summary>
        /// Get Advance Value - return httpResult with String type
        /// Developer can hide this route in help page by put [ApiExplorerSettings(IgnoreApi = true)]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetAdvanceValue")]
        public IHttpActionResult Get(int id)
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return Ok(String.Format("Hello, {0}.", userName));
        }


        /// <summary>
        /// Get ResponseBase Message example and support type in help page by put attribute [ResponseType(typeof(xClass))]
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("GetResponseBase")]
        [ResponseType(typeof(ResponseBase))]
        public async Task<IHttpActionResult> GetResponseBase(int offset, int limit)
        {
            return Ok(new ResponseBase
            {
                devMessage = "put developer message here",
                userMessage = "put user message here",
                statusCode = 200
            });
        }

        /// <summary>
        /// Get ResponseBase Message example and support type in help page by put attribute [ResponseType(typeof(xClass))]
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("GetResponseBase2")]
        [ResponseType(typeof(ResponseBase<Person>))]
        [Route("{offset}/{limit}")]
        public async Task<IHttpActionResult> GetResponseBase2(int offset, int limit)
        {
            Person p = new Person
            {
                MyProperty = 1,
                MyProperty2 = 2,
                MyProperty3 = "Greg",
                MyProperty4 = "Young"
            };

            return Ok(new ResponseBase<Person>
            {
                devMessage = "put developer message here",
                userMessage = "put user message here",
                statusCode = 200,
                item = p
            });
        }

        /// <summary>
        /// Get ListResponseBase Message example and support type in help page by put attribute [ResponseType(typeof(xClass))]
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("GetResponseBase3")]
        [ResponseType(typeof(ListResponseBase<Person>))]
        [Route("{offset}/{limit}")]
        public async Task<IHttpActionResult> GetResponseBase3(int offset, int limit)
        {
            List<Person> ps = new List<Person>();
            Person p = new Person
            {
                MyProperty = 1,
                MyProperty2 = 2,
                MyProperty3 = "Greg",
                MyProperty4 = "Young"
            };

            ps.Add(p);
            ps.Add(p);

            return Ok(new ListResponseBase<Person>
            {
                devMessage = "put developer message here",
                userMessage = "put user message here",
                statusCode = 200,
                items = ps
            });
        }
    }
}
