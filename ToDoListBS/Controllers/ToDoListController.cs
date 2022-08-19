using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Generally.Repositories;
using Generally.Data;

namespace ToDoListBS.Controllers
{
    [ApiController]
    //有版本號的使用方法
    //[ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/[controller]/[action]")]
    //[EnableCors("NodjsEx")]曾對個別的control套用cors
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        IConfiguration _config;
        ToDoListRepository tdlr = null;
        private readonly ILogger<ToDoListController> _logger;
        public ToDoListController(ILogger<ToDoListController> logger, IConfiguration config)
        {
            _config = config;
            tdlr = new ToDoListRepository(_config);
            _logger = logger;
        }

        //[DisableCors]
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<ToDoList> listToDoList = await tdlr.GetAll();
                return Ok(listToDoList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
            }
        }


        //[TypeFilter(typeof(Filters.ActionFilter))]
        //[MiddlewareFilter(typeof(Middleware.ReqHeaderChecker))]
        //[HttpHead]
        [HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        public async Task<ActionResult<ToDoList>> Get(int id)
        {
            try
            {
                ToDoList? tdl = await tdlr.Get(id);
                if (tdl == null)
                {
                    return NotFound();
                }
                return Ok(tdl);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ToDoList>>> Search(string title, string? description)
        {
            try
            {
                var result = await tdlr.Search(title, description);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "db連線錯誤");
            }
        }


        #region 新增  不回傳資料
        /// <summary>
        /// 新增  不回傳資料
        /// </summary>
        /// <param name="ptdl"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Post([FromBody] ToDoList ptdl)
        //{
        //    try
        //    {
        //        if (ptdl == null)
        //            return BadRequest();
        //        int iResult = await tdlr.Add(ptdl);
        //        if (iResult > 0)
        //            return Ok(ptdl);
        //        else
        //            return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogWarning(ex, "ToDoList 新增失敗");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
        //    }
        //}v
        #endregion

        /// <summary>
        /// 回傳原新增資料並回傳其他資料
        /// </summary>
        /// <param name="ptdl"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post2ReturnData([FromBody] ToDoList ptdl)
        {
            try
            {
                if (ptdl == null)
                    return BadRequest();
                ToDoList tdlResult = await tdlr.AddRIdentity(ptdl);
                if (tdlResult != null)
                {
                    return CreatedAtAction(nameof(Get), new { id = tdlResult.Id }, tdlResult);
                    //return new CreatedResult($"/products/{ToDoList.ProductNumber.ToLower()}", tdl);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "ToDoList 新增失敗");
                //return ValidationProblem(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "存取資料時發生錯誤");
                //return BadRequest();
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        //public async Task<ActionResult<ToDoList>> Put(int id, [FromBody] ToDoList tdlist)
        public async Task<IActionResult> Put(int id, [FromBody] ToDoList tdlist)
        {
            try
            {
                if (id != tdlist.Id)
                {
                    return BadRequest($"更新ID={id}不匹配");
                }
                var tdl = tdlr.Get(id);
                if (tdl == null)
                {
                    return NotFound($"ToDoList id ={id} 找不到");
                }
                int iResult = await tdlr.Update(tdlist);
                if (iResult <= 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "更新失敗");
                }
                else
                {
                    return Ok(tdlist);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "ToDoList 新增失敗");
                return StatusCode(StatusCodes.Status500InternalServerError, "更新失敗");
            }
        }


        /// <summary>
        /// 僅針對修改的部分更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<string> ToDoList(int id)
        {
            string sResult = "";
            return sResult;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tdl = await tdlr.Get(id);
                if (tdl == null)
                {
                    return NotFound($"todolist with id={id} 找不到");
                }
                int iResult = await tdlr.Delete(id);
                if (iResult > 0)
                {
                    return Ok($"todolist with id={id} 刪除成功");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "刪除失敗");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "刪除失敗");
            }

        }


    }
}
