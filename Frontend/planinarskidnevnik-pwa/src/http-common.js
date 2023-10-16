import axios from "axios";


export default axios.create({
    baseURL: "https://domagojm95-001-site1.atempurl.com/Zavrsni/v1",
    headers: {
        "Content-Type": "application/json"
    }
});