﻿global using System.Diagnostics;
global using AutoMapper;
global using GestioneSagre.EFCore.Extensions;
global using GestioneSagre.EFCore.GenericRepository;
global using GestioneSagre.EFCore.GenericRepository.Interfaces;
global using GestioneSagre.EFCore.Models.Enums;
global using GestioneSagre.EFCore.Models.Options;
global using GestioneSagre.EFCore.UnitOfWork;
global using GestioneSagre.EFCore.UnitOfWork.Interfaces;
global using GestioneSagre.GenericServices.Extensions;
global using GestioneSagre.Prodotti.BusinessLayer.Mappers;
global using GestioneSagre.Prodotti.BusinessLayer.Services;
global using GestioneSagre.Prodotti.DataAccessLayer;
global using GestioneSagre.Prodotti.DataAccessLayer.Entities;
global using GestioneSagre.Prodotti.Shared.Models.InputModels;
global using GestioneSagre.Prodotti.Shared.Models.ViewModels;
global using GestioneSagre.Shared.OperationResults;
global using Hellang.Middleware.ProblemDetails;
global using Hellang.Middleware.ProblemDetails.Mvc;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.Server.Kestrel.Core;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Logging;