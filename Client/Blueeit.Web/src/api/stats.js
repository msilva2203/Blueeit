const API_URL = import.meta.env.VITE_API_URL;

export async function getStats() {
    const response = await fetch(
        `${API_URL}/stats`
    );

    if (!response.ok) {
        throw new Error("Failed to fetch stats");
    }

    return await response.json();
}