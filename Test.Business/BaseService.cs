using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Interfaces.Repositories;
using Test.Interfaces.Services;

namespace Test.Business
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        //Declaración de variables globales
        private readonly IBaseRepository<TEntity> baseRepository;

        /**
         * <summary>Método constructor</summary>
         * <param name="baseRepository">Corresponde a la interfaz de tipo IBaseRepository</param>
         */
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }//Fin del método

        /**
         * <summary>Método que permite ingresar una entidad</summary>
         * <param name="entity">Corresponde a la entidad que se desea agregar</param>
         */
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            return await this.baseRepository.Add(entity);
        }//Fin del método

        /**
         * <summary>Método que permite eliminar una entidad</summary>
         * <param name="id">Corresponde al identificador de la entidad que se desea eliminar</param>
         */
        public virtual async Task Delete(int id)
        {
            await this.baseRepository.Delete(id);
        }//Fin del método



        /**
         * <summary>Método que permite obtener todos los registros pertenecientes a esa entidad</summary>
         * <returns>Todos los objetos de ese tipo de entidad</returns>
         */
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await this.baseRepository.GetAll();
        }//Fin del método

        /**
         * <summary>Método que permite obtener la información correspondiente a la entidad solicitada</summary>
         * <param name="id">Corresponde al identificador de la entidad que se desea obtener</param>
         * <returns>La información de la correspondiente entidad</returns>
         */
        public virtual async Task<TEntity> GetById(int id)
        {
            return await this.baseRepository.GetById(id);
        }//Fin del método

        /**
         * <summary>Método que permite actualizar la información una entidad</summary>
         * <param name="entity">Corresponde a la entidad que se desea modificar</param>
         */
        public virtual async Task<TEntity> Modify(TEntity entity)
        {
            return await this.baseRepository.Modify(entity);
        }//Fin del método
    }
}
