import { apiFetch } from "./apiFetch";
import { useAuthStore } from "../stores/authStore";
import { refreshAccessToken } from "./token";

export async function authFetch(
  path: string,
  init: RequestInit = {}
): Promise<Response> {
  const { accessToken, logout } = useAuthStore.getState();

  const doFetch = async (token?: string | null) => {
    const headers = new Headers(init.headers || {});
    const finalToken = token ?? useAuthStore.getState().accessToken;

    if (finalToken) {
      headers.set("Authorization", `Bearer ${finalToken}`);
    }

    return apiFetch(path, {
      ...init,
      headers,
    });
  };

  let response = await doFetch(accessToken);

  if (response.status !== 401) {
    return response;
  }

  const newToken = await refreshAccessToken();

  if (!newToken) {
    logout();
    return response;
  }

  response = await doFetch(newToken);

  if (response.status === 401 || response.status === 403) {
    logout();
  }

  return response;
}
