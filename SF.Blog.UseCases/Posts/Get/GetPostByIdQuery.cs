﻿using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public record GetPostByIdQuery(string Id) : IRequest<Result<Post>>;
