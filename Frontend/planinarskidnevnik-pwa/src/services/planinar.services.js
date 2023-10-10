import http from "../http-common";


class PlaninarDataService{

    async get(){
        return await http.get('/Planinar');
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

}

export default new PlaninarDataService();