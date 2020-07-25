import axios from "axios"
import config from 'config';
import { authHeader } from '../_helpers';
const baseURL= config.apiUrl;

const user= JSON.parse(localStorage.getItem('user'));

export default {
    DRecipes(url= baseURL + '/api/recipes/'){
        return{
            fetchAll: ()=> axios.get(url, {
                headers: {
                    'Authorization': `token ${user.token}`} 
                }),
            fetchById: id => axios.get(url + id),
            create: newRecord=> axios.post(url, newRecord),
            update: (id,updateRecord)=> axios.put(url+ id, updateRecord),
            partialUpdate: (id, updateRecord)=> axios.patch(url + id, updateRecord),
            delete: id=> axios.delete(url + id)
        }
    }
}

