using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GetPanamaDB.models;
using GetPanamaDB.models.modelviews;
using GetPanamaDB;

using System.Reflection;
using Common.Logging;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Web;

namespace GetPanamaDBWebApi.Controllers
{

    public class PanamadbController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private GetPanamaDbContext db = new GetPanamaDB.models.GetPanamaDbContext();

        private static Dictionary<string, bool> getEntity = new Dictionary<string, bool>();
        private static Dictionary<string, bool> getConnection = new Dictionary<string, bool>();
        private static Dictionary<string, bool> getCountryItems = new Dictionary<string, bool>();

        // GET: api/entityCommMain
        public EntityConnect Get()
        {
            try
            {
                logger.Info("OK");

                var viewmodels = new EntityConnect();
                viewmodels.connections = db.Connections.Take(10).ToList();
                viewmodels.entitysAll = db.EntitysAll.FirstOrDefault();
                return viewmodels;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        [Route("api/getLast")]
        [ResponseType(typeof(CountryItems))]
        public CountryItems lastAddCountry()
        {
            try
            {
                var tmpModelsLast = db.CountryItems.OrderByDescending(m => m.addDate).Take(1).FirstOrDefault();
                return tmpModelsLast;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        [Route("api/topCountryItems/{id}")]
        [ResponseType(typeof(ICollection<CountryItems>))]
        public IQueryable<CountryItems> topCountryItems(int id)
        {
            try
            {
                if (id > 1000)
                {
                    id = 1000;
                }
                var tmpModelsLast = db.CountryItems.Where(m => m.tStatus == 0).Take(id);
                return tmpModelsLast;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // POST: api/CountryItems
        [HttpPost]
        [Route("api/CountryItems")]
        [ResponseType(typeof(ICollection<CountryItems>))]
        public async Task<IHttpActionResult> PCountryItems(ICollection<CountryItems> viewmodels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                foreach (var item in viewmodels)
                {
                    item.ClientIP = HttpContext.Current.Request.UserHostAddress;
                    item.UpdateDate = DateTime.Now;

                    if (!string.IsNullOrEmpty(item.Countries) && !string.IsNullOrEmpty(item.name))
                    {
                        if (!getCountryItems.ContainsKey(item.name))
                        {
                            getCountryItems.Add(item.name, true);

                            var exitDB = db.CountryItems.Count(m => m.name.Equals(item.name) && m.Countries.Equals(item.Countries));
                            if (exitDB <= 0)
                            {
                                db.CountryItems.Add(item);
                            }
                        }

                    }
                }
                //end
                await db.SaveChangesAsync();

                if (getCountryItems.Count > 50)
                {
                    getCountryItems.Clear();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }


        // POST: api/entityCommMain
        [ResponseType(typeof(EntityConnect))]
        public async Task<IHttpActionResult> Post(EntityConnect viewmodels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //save Entity
                if (viewmodels.entitysAll != null)
                {
                    if (!string.IsNullOrEmpty(viewmodels.entitysAll.name) && !string.IsNullOrEmpty(viewmodels.entitysAll.ttype))
                    {
                        var toExitMain = db.EntitysAll.Count(m => m.name.Equals(viewmodels.entitysAll.name));
                        if (toExitMain <= 0)
                        {
                            viewmodels.entitysAll.ClientIP = HttpContext.Current.Request.UserHostAddress;
                            viewmodels.entitysAll.UpdateDate = DateTime.Now;

                            if (!getEntity.ContainsKey(viewmodels.entitysAll.name))
                            {
                                getEntity.Add(viewmodels.entitysAll.name, true);
                                //save db
                                db.EntitysAll.Add(viewmodels.entitysAll);                               
                            }
                        }
                        else
                        {
                            if (viewmodels.entitysAll.getPage > 0)
                            {
                                var getExit = db.EntitysAll.Where(m => m.name.Equals(viewmodels.entitysAll.name)).FirstOrDefault();
                                if (getExit != null)
                                {
                                    if (viewmodels.entitysAll.getPage > getExit.getPage)
                                    {
                                        getExit.getPage = viewmodels.entitysAll.getPage;
                                    }
                                }

                            }
                        }
                    }
                }

                //save connect

                if (viewmodels.connections != null)
                {
                    if (viewmodels.connections.Count > 0)
                    {
                        foreach (var item in viewmodels.connections)
                        {
                            if (!string.IsNullOrEmpty(item.nameFrom) && !string.IsNullOrEmpty(item.nameType) && !string.IsNullOrEmpty(item.nameTo))
                            {
                                item.ClientIP = HttpContext.Current.Request.UserHostAddress;
                                item.UpdateDate = DateTime.Now;
                                //check the same
                                var tmpKey = string.Concat(item.nameFrom, item.nameType, item.nameTo);
                                if (getConnection.ContainsKey(tmpKey))
                                {
                                    continue;
                                }
                                else
                                {
                                    getConnection.Add(tmpKey, true);
                                }


                                var toExitMain = db.Connections.Count(m => m.nameFrom.Equals(item.nameFrom) && m.nameType.Equals(item.nameType) && m.nameTo.Equals(item.nameTo));
                                if (toExitMain <= 0)
                                {
                                    db.Connections.Add(item);
                                }

                            }
                        }
                    }

                }

                //change status
                var tmpCurrCountry = db.CountryItems.Where(m => m.Tid == viewmodels.Tid).FirstOrDefault();
                if (tmpCurrCountry != null)
                {
                    tmpCurrCountry.tStatus = 1;
                }

                //end
                await db.SaveChangesAsync();

                if (getEntity.Count > 50)
                {
                    getEntity.Clear();
                }
                if (getConnection.Count > 50)
                {
                    getConnection.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
