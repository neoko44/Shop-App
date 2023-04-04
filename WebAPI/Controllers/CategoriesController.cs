﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers //apilerde isimler çoğul yazılır(ProductsController gibi)
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;


        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _categoryService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getcategorybyname")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            var result = _categoryService.GetByName(categoryName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        //[HttpGet("getproductsbycategoryid")]
        //public IActionResult GetListByCategory(int categoryId)
        //{
        //    var result = _categoryService.GetAllLists(categoryId);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        //[HttpGet("getbyid")]
        //public IActionResult GetById(int categoryId)
        //{
        //    var result = _categoryService.GetById(categoryId);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        //[HttpPost("add")]
        
        //public IActionResult Add(Category category)
        //{
        //    var result = _categoryService.Add(category);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    return BadRequest(result.Message);
        //}

        //[HttpPost("update")]
        //public IActionResult Update(Category category)
        //{
        //    var result = _categoryService.Update(category);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    return BadRequest(result.Message);
        //}

        //[HttpPost("delete")]
        //public IActionResult Delete(Category category)
        //{
        //    var result = _categoryService.Delete(category);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    return BadRequest(result.Message);
        //}
    }

}
