﻿using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetCategories;

public record GetCategoriesQuery(int PageNumber, int PageSize) : IRequest<Result>;
