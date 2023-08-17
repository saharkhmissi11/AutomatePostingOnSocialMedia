import axios from 'axios';

const corsAnywhereUrl = 'https://cors-anywhere.herokuapp.com/'; // Use the cors-anywhere proxy URL

const api = axios.create({
  baseURL: `${corsAnywhereUrl}https://localhost:7147/api/`, // Update with your API base URL
  headers: {
    'accept': 'application/json',
  },
});

export const getImagesLinks = async () => {
  try {
    const response = await api.get('DropBox/GetImagesLinks');
    return response.data;
  } catch (error) {
    console.error('Error fetching data:', error);
    throw error;
  }
};
