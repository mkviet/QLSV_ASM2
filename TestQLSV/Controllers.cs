using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using QLSVVV.Controllers;
using QLSVVV.Data;
using QLSVVV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QLSVVV.Tests.Controllers
{
    public class TeachersControllerTests
    {
        private DbContextOptions<QLSVVVContext> _dbContextOptions;
        private QLSVVVContext _context;
        private TeachersController _controller;

        public TeachersControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<QLSVVVContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new QLSVVVContext(_dbContextOptions);
            _controller = new TeachersController(_context);
        }
        // 1 ki?m  th? c?a xác nh?n r?ng ph??ng th?c Index c?a controller ho?t ??ng ?úng
        [Fact]
        public async Task Index_ReturnsViewWithTeachers()
        {
            // Arrange
            _context.Teacher.Add(new Teacher { Id = 1, Name = "Dinh van dong", DoB = DateTime.Now, Major = "IT" });
            _context.Teacher.Add(new Teacher { Id = 2, Name = "HA NGOC LINH", DoB = DateTime.Now, Major = "IT" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Teacher>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        // ki?m th? ch?c n?ng create
        [Fact]
        public async Task Create_ValidModel_RedirectsToManageTeacher()
        {
            // Arrange
            var teacher = new Teacher { Id = 3, Name = "Teacher3", DoB = DateTime.Now, Major = "English" };

            // Act
            var result = await _controller.Create(teacher);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageTeacher", redirectToActionResult.ActionName);
        }
        // 
        [Fact]
        public async Task Edit_ValidModel_RedirectsToManageTeacher()
        {
            // Arrange
            var teacher = new Teacher { Id = 4, Name = "Teacher4", DoB = DateTime.Now, Major = "History" };
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            // Act
            teacher.Name = "UpdatedTeacher";
            var result = await _controller.Edit(teacher.Id, teacher);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageTeacher", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_ValidId_RedirectsToManageTeacher()
        {
            // Arrange
            var teacher = new Teacher { Id = 5, Name = "Teacher5", DoB = DateTime.Now, Major = "Geography" };
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(teacher.Id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageTeacher", redirectToActionResult.ActionName);
        }
    }

    public class StudentsControllerTests
    {
        private DbContextOptions<QLSVVVContext> _dbContextOptions;
        private QLSVVVContext _context;
        private StudentsController _controller;

        public StudentsControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<QLSVVVContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new QLSVVVContext(_dbContextOptions);
            _controller = new StudentsController(_context);
        }

        [Fact]
        public async Task Index_ReturnsViewWithStudents()
        {
            // Arrange
            _context.Student.Add(new Student { Id = 1, Name = "bienz", DoB = DateTime.Now, Address = "bavi", Major = "asdads" });
            _context.Student.Add(new Student { Id = 2, Name = "bien nguyen", DoB = DateTime.Now, Address = "coido", Major = "IT" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToManageStudent()
        {
            // Arrange
            var student = new Student { Id = 3, Name = "Student3", DoB = DateTime.Now, Address = "Address3", Major = "English" };

            // Act
            var result = await _controller.Create(student);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageStudent", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ValidModel_RedirectsToManageStudent()
        {
            // Arrange
            var student = new Student { Id = 4, Name = "Student4", DoB = DateTime.Now, Address = "Address4", Major = "History" };
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            // Act
            student.Name = "UpdatedStudent";
            var result = await _controller.Edit(student.Id, student);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageStudent", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_ValidId_RedirectsToManageStudent()
        {
            // Arrange
            var student = new Student { Id = 5, Name = "Student5", DoB = DateTime.Now, Address = "Address5", Major = "Geography" };
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(student.Id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageStudent", redirectToActionResult.ActionName);
        }
    }
}
