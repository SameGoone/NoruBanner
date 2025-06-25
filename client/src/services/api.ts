import axios from 'axios';
import type { BannerEvent } from '../models/BannerEvent';

axios.defaults.baseURL = import.meta.env.VITE_API_BASE_URL;

export const sendViewEvent = (event: BannerEvent) => {
    return axios.post('/events/view', event);
};

export const sendClickEvent = (event: BannerEvent) => {
    return axios.post('/events/click', event);
};