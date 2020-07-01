using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.DesignPattern
{


    public abstract class IStepCancellationHandler
    {
        public int ReservationNum { get; set; }
        public int ReservationId { get; set; }
        public int CurrentStep { get; set; }
        public abstract void Notify(int[] ids);
        public abstract void AssignAgent(int id);
        public abstract void FillForm();
        public abstract void Complete();

      //  public abstract void NextStep(int step);
        public  void NextStep(int Currentstep)
        {
            // check current step status from log >> isdone or not  

            // get next step 

            // if exist  >> call step action 

        }
    }


    public class CancelledByCustomer : IStepCancellationHandler
    {

        public override void Notify(int[] ids)
        {
            // send notification  

            NextStep(CurrentStep);

            throw new NotImplementedException();
        }

        public override void AssignAgent(int id  )
        {
            //  assign 

            throw new NotImplementedException();
        }

        public override void FillForm()
        {
            throw new NotImplementedException();
        }

        public override void Complete()
        {
            throw new NotImplementedException();
        }

       


    }



}
