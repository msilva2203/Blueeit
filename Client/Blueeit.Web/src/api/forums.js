const API_URL = import.meta.env.VITE_API_URL;

export async function getForums(page = 1, pageSize = 20) {
    const response = await fetch(
        `${API_URL}/forums?page=${page}&${pageSize}`
    );

    if (!response.ok) {
        throw new Error("Failed to fetch forums");
    }

    return await response.json();
}