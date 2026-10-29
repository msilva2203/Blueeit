const API_URL = import.meta.env.VITE_API_URL;

export async function getLatestProfilePosts(page = 1, pageSize = 20) {
    const response = await fetch(
        `${API_URL}/profile-posts?key=newest&page=${page}&${pageSize}`
    );

    if (!response.ok) {
        throw new Error("Failed to fetch latest profile posts");
    }

    return await response.json();
}