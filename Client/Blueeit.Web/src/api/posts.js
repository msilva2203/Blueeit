const API_URL = import.meta.env.VITE_API_URL;

export async function getLatestPosts(page = 1, pageSize = 20) {
    const response = await fetch(
        `${API_URL}/posts?key=newest&page=${page}&${pageSize}`
    );

    if (!response.ok) {
        throw new Error("Failed to fetch latest posts");
    }

    return await response.json();
}