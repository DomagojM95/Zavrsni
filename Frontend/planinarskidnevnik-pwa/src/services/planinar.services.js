import http from "../http-common";


class PlaninarDataService{

    async get(){
        return await http.get('/Planinar');
    }

    async getBySifra(sifra) {
        return await http.get('/smjer/' + sifra);
      }


    async delete(sifra){
        const odgovor = await http.delete('/Planinar/' + sifra)
        .then(response => {
            return {ok: true, poruka: 'Obrisao uspješno'};
        })
        .catch(e=>{
            return {ok: false, poruka: e.response.data};
        });

        return odgovor;
    }

    async post(planinar){
        //console.log(smjer);
        const odgovor = await http.post('/planinar',planinar)
           .then(response => {
             return {ok:true, poruka: 'Unio planinara'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
    }

}

export default new PlaninarDataService();