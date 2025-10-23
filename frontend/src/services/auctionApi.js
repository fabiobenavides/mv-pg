import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    timeout: 10000,
    headers: { 'Content-Type': 'application/json' }
});

/**
 * Call the /calculate-fees endpoint with basePrice and vehicleType.
 * @param {number} basePrice - The base price to calculate fees from.
 * @param {string} vehicleType - The type of vehicle (e.g. "car", "motorcycle").
 * @returns {Promise<any>} - The response data from the API.
 */
export async function calculateFees(basePrice, vehicleType) {
    try {
        const response = await api.post('/calculate-fees', { basePrice, vehicleType });
        return response.data;
    } catch (error) {
        if (error.response) {
            const message = error.response.data && error.response.data.message
                ? error.response.data.message
                : `Request failed with status ${error.response.status}`;
            throw new Error(message);
        }
        throw error;
    }
}

export default { calculateFees };