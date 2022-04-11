using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Model;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class FilmesApplicationServices
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public FilmesApplicationServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddFilme(CreateFilmeDto _filme)
        {
            Filme filme = _mapper.Map<Filme>(_filme);
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            return filme.id;
        }

        public async Task<List<ReadFilmeDto>> RecuperaFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)            
                filmes = await _context.Filmes.ToListAsync();            
            else
                filmes =  await _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToListAsync();
            if(filmes.Count != 0)
            {
                List<ReadFilmeDto> dto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return dto;
            }
            return null;
        }

        public async Task<ReadFilmeDto> RecuperaFilmesById(int id)
        {
            Filme filme = await _context.Filmes.FirstOrDefaultAsync(x => x.id == id);
            ReadFilmeDto readFilme = _mapper.Map<ReadFilmeDto>(filme);
            readFilme.DataHoraConsulta = DateTime.Now;
            return readFilme;
        }

        public async Task<Result> AtualizaFilme(int id, UpdateFilmeDto filmeAtualizado)
        {
            Filme filme = await _context.Filmes.FirstOrDefaultAsync(x => x.id == id);
            if (filme == null)
                return Result.Fail("Filme não encontrado!");

            _mapper.Map(filmeAtualizado, filme);

            await _context.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DeletaFilmeById(int id)
        {
            Filme filme = await _context.Filmes.FirstOrDefaultAsync(x => x.id == id);
            if (filme == null)
                return Result.Fail("Filme não encontrado!");

            _context.Remove(filme);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
