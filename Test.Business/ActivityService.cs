using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Business;
using Test.Entities;

namespace Test.BusinessLogic
{


    public class ActivityService : BaseService<Entities.Activity>, Interfaces.Services.IActivityServices
    {
        //Declaración de variables globales
        private readonly Interfaces.Repositories.IActivityRepository activityRepository;
        private readonly Interfaces.Repositories.IPropertyRepository propertyRepository;


        /**
         * <summary>Método constructor</summary>
         * <param name="taskRepository">Corresponde al tipo de interfaz de tipo ITaskRepository</param>
         */
        public ActivityService(Interfaces.Repositories.IActivityRepository activityRepository, Interfaces.Repositories.IPropertyRepository propertyRepository) : base(activityRepository)
        {
            this.activityRepository = activityRepository;
            this.propertyRepository = propertyRepository;
        }//Fin del método


        public async override Task<Activity> Add(Activity entity)
        {

            //validamos que la propiedad este activa
            var property = await this.propertyRepository.GetById(entity.Property_Id);
            if (property == null)
                throw new Exception("Property invalid");

            if (property.Property_Status.Equals("Disabled", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Property disabled");
            }

            //verificamos que no haya una actividad en la misma hora para la misma propiedad
            var activityExists = await this.activityRepository.Exists(entity.Property_Id, entity.Activity_Schedule);
            if (activityExists != null)
            {
                throw new Exception("There is already an activity programmed for that time");
            }


            if (string.IsNullOrEmpty(entity.Activity_Title))
            {
                throw new Exception("Activity_Title is required");
            }

            if (entity.Activity_Schedule.Equals(default(DateTime)))
            {
                throw new Exception("Activity_Schedule is required");
            }

            var _activity = await base.Add(entity);

            var _newActivity = await base.GetById(_activity.Activity_Id);

            return _newActivity;
        }

        public  async Task<Activity> Cancel(Entities.Activity entity)
        {
            //verificamos que la activida exista
            var activity = await base.GetById(entity.Activity_Id);
            if (activity == null)
            {
                throw new Exception("The activity does not exist");
            }


            if (activity.Activity_Status.Equals("Cancelled"))
            {
                throw new Exception("It is not possible to cancel an activity previously canceled");
            }

            await this.activityRepository.Cancel(entity);
            activity = await base.GetById(entity.Activity_Id);

            return activity;

        }

        public async Task<IEnumerable<Activity>> GetByFilters(Parameters parameters)
        {
            return await this.activityRepository.GetByFilters(parameters);
        }

        public async Task<Activity> Reschedule(Entities.Activity entity)
        {
            //verificamos que la activida exista
            var activity = await base.GetById(entity.Activity_Id);
            if (activity == null)
            {
                throw new Exception("The activity does not exist");
            }

            if (activity.Activity_Status.Equals("Cancelled"))
            {
                throw new Exception("It is not possible to schedule the activity because it has been canceled");
            }


            //verificamos que no haya una actividad en la misma hora para la misma propiedad
            var activityExists = await this.activityRepository.Exists(activity.Property_Id, entity.Activity_Schedule);
            if (activityExists != null)
            {
                throw new Exception("There is already an activity programmed for that time");
            }

            await this.activityRepository.Reschedule(entity);
            activity = await base.GetById(entity.Activity_Id);

            return activity;

        }
    }

}
