using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Model;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class GerenteApplicationServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GerenteApplicationServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadGerenteDto> AdicionarGerente(CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            await _context.AddAsync(gerente);
            await _context.SaveChangesAsync();
            ReadGerenteDto readGerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            return readGerenteDto;
        }

        public async Task<ReadGerenteDto> RecuperaGerentePorId(int id)
        {
            Gerente gerente = await _context.Gerentes.FirstOrDefaultAsync(x => x.Id == id);
            if (gerente == null)
                return null;
            ReadGerenteDto dto = _mapper.Map<ReadGerenteDto>(gerente);
            return dto;

        }

        public async Task<Result> DeletaGerente(int id)
        {
            Gerente gerente = await _context.Gerentes.FirstOrDefaultAsync(x => x.Id == id);
            if (gerente == null)
                return Result.Fail("Gerente não encontrado!");
            _context.Remove(gerente);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
