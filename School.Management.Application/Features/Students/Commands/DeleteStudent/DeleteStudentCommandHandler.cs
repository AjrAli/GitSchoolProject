﻿using AutoMapper;
using MediatR;
using SchoolProject.Management.Application.Contracts.Persistence;
using SchoolProject.Management.Application.Exceptions;
using SchoolProject.Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.EntityFrameworkCore;

namespace SchoolProject.Management.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeleteStudentCommandResponse>
    {
        private readonly IBaseRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentCommandHandler(IMapper mapper, IBaseRepository<Student> studentRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteStudentCommandResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var deleteStudentCommandResponse = new DeleteStudentCommandResponse();
            try
            {
                long Id = (long)request.StudentId;
                var studentToDelete = await _studentRepository.GetAsync(Id);

                if (studentToDelete == null)
                    throw new NotFoundException(nameof(Student), request.StudentId); ;


                await _studentRepository.DeleteAsync(studentToDelete);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                    deleteStudentCommandResponse.Message = "ok";
                else
                    deleteStudentCommandResponse.Message = "Not ok";
            }
            catch (Exception ex)
            {
                deleteStudentCommandResponse.Message = $"ERROR : {ex.InnerException?.Source} : {ex.InnerException?.Message}";
            }
            return deleteStudentCommandResponse;
        }
    }
}
