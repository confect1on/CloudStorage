﻿using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

public record DeleteFileCommand(FileMetadataId FileMetadataId) : IRequest;
