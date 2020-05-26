using LearningCore.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCore.MVC.Filters
{
    /// <summary>
    /// 从配置系统读取标题和名称。 与前面的示例不同，以下代码不需要将筛选器参数添加到代码中。
    /// </summary>
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        private readonly WxConfigModel _settings;

        public MyActionFilterAttribute(IOptions<WxConfigModel> options)
        {
            _settings = options.Value;
            Order = 1;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //将标题和名称添加到响应标头
            context.HttpContext.Response.Headers.Add(_settings.AppId,
                                                     new string[] { _settings.AppSecret });
            base.OnResultExecuting(context);
        }
    }
}
