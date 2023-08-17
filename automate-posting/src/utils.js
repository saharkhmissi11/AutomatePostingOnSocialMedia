import axios from "axios";

const client = axios.create({
  baseURL: "https://localhost:7147/api/" 
});

export const headers = {
    'Content-Type': 'application/json',
    Authorization: '',
};

export const config = { headers: headers };

export default client;