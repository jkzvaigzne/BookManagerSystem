﻿using AutoMapper;
using BookManagerSystem.Web.Data;
using BookManagerSystem.Web.Models.Books;

namespace BookManagerSystem.Web.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookReadOnlyVM>();
            CreateMap<BookCreateVM, Book>();
            CreateMap<BookEditVM, Book>().ReverseMap();
        }
    }
}
