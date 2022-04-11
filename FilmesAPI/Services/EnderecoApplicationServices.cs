using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Model;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class EnderecoApplicationServices
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoApplicationServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AdicionaEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();
            return endereco.Id;
        }

        public async Task<List<ReadEnderecoDto>> RecuperaEnderecos()
        {
            List<Endereco> endereco = await _context.Enderecos.ToListAsync();
            if(endereco.Count > 0)
            {
                List<ReadEnderecoDto> readEnderecoDtos = new List<ReadEnderecoDto>();
                foreach (Endereco item in endereco)
                {
                    ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(item);
                    readEnderecoDtos.Add(enderecoDto);
                }
                return readEnderecoDtos;
            }
            return null;
        }

        public async Task<ReadEnderecoDto> RecuperaEnderecosPorId(int id)
        {
            Endereco endereco =  await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == id);
            if(endereco != null)
            {
                ReadEnderecoDto readDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return readDto;
            }
            return null;
        }

        public async Task<bool> AtualizaEndereco(int id, UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == id);
            if (endereco != null)
            {
                endereco = _mapper.Map<Endereco>(enderecoDto);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Result> DeletaEndereco(int id)
        {
            Endereco endereco = await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == id);
            if (endereco != null)
            {
                _context.Remove(endereco);
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            return Result.Fail("Falha ao deletar endereço!");
        }
    }
}
