import http from "../http-common";


class DnevnikDataService{

    async get(){
        return await http.get('/dnevnik');
    }

    async getBySifra(sifra) {
        return await http.get('/dnevnik/' + sifra);
      }

    async delete(sifra){
        const odgovor = await http.delete('/Dnevnik/' + sifra)
        .then(response => {
            return {ok: true, poruka: 'Obrisao uspješno'};
        })
        .catch(e=>{
            return {ok: false, poruka: e.response.data};
        });

        return odgovor;
    }


    async post(dnevnik){
        //console.log(smjer);
        const odgovor = await http.post('/dnevnik',dnevnik)
           .then(response => {
             return {ok:true, poruka: 'Unio dnevnik'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
    }

    async put(sifra,dnevnik){
        
        const odgovor = await http.put('/dnevnik/' + sifra,dnevnik)
           .then(response => {
             return {ok:true, poruka: 'Promjenio dnevnik'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
         }

}

export default new DnevnikDataService();