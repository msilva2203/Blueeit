import { useEffect } from "react";

import { usePaginatedState } from "../hooks/usePaginatedState";
import { getLatestPosts } from "../api/posts";
import { getLatestProfilePosts } from "../api/profilePosts";
import { getLatestActivity } from "../api/userActivity";

import Block from "../components/Block";
import BlockContent from "../components/BlockContent";
import BlockHeader from "../components/BlockHeader";
import BlockSpacer from "../components/BlockSpacer";
import BodyHeader from "../components/BodyHeader";
import PageLayout from "../components/PageLayout";
import StatsPanel from "../components/panels/StatsPanel";
import ErrorBlock from "../components/ErrorBlock";
import ProfilePostNodeList from "../components/ProfilePostNodeList";
import ActivityNodeList from "../components/nodes/activityNodes/ActivityNodeList";

export default function WhatsNewPage() {
    const page = 1;
    const pageSize = 10;

    const latestPosts = usePaginatedState({
        initialPage: page
    });
    const latestProfilePosts = usePaginatedState({
        initialPage: page
    });
    const latestActivity = usePaginatedState({
        initialPage: page
    });

    useEffect(() => {
        async function fetchData() {
            latestPosts.setIsLoading(true);
            latestProfilePosts.setIsLoading(true);
            latestActivity.setIsLoading(true);

            try {
                const [posts, profilePosts, activity] = await Promise.all([
                    getLatestPosts(latestPosts.page, pageSize),
                    getLatestProfilePosts(latestProfilePosts.page, pageSize),
                    getLatestActivity(latestActivity.page, pageSize)
                ]);

                latestPosts.setData(posts);
                latestProfilePosts.setData(profilePosts);
                latestActivity.setData(activity);

                console.log(posts.items);
                console.log(profilePosts.items);
                console.log(activity.items);
            } catch (error) {
                console.error(error);

                latestPosts.setError(error);
                latestProfilePosts.setError(error);
                latestActivity.setError(error);
            } finally {
                latestPosts.setIsLoading(false);
                latestProfilePosts.setIsLoading(false);
                latestActivity.setIsLoading(false);
            }
        }

        fetchData();
    }, [])

    function renderLatestPosts() {
        if (latestPosts.isLoading) {
            return (
                <BlockContent>
                    <small>
                        Loading...
                    </small>
                </BlockContent>
            );
        }

        if (latestPosts.error != null) {
            return (
                <BlockContent>
                    <ErrorBlock>
                        <BlockContent>
                            <small className="blueeit-error">
                                Unable to load latest posts.
                            </small>
                        </BlockContent>
                    </ErrorBlock>
                </BlockContent>
            );
        }

        return (
            <BlockContent />
        )
    }

    function renderLatestProfilePosts() {
        if (latestProfilePosts.isLoading) {
            return (
                <BlockContent>
                    <small>
                        Loading...
                    </small>
                </BlockContent>
            );
        }

        if (latestProfilePosts.error != null) {
            return (
                <BlockContent>
                    <ErrorBlock>
                        <BlockContent>
                            <small className="blueeit-error">
                                Unable to load latest profile posts.
                            </small>
                        </BlockContent>
                    </ErrorBlock>
                </BlockContent>
            );
        }

        return (
            <ProfilePostNodeList posts={latestProfilePosts.items} />
        )
    }

    function renderLatestActivity() {
        if (latestActivity.isLoading) {
            return (
                <BlockContent>
                    <small>
                        Loading...
                    </small>
                </BlockContent>
            );
        }

        if (latestActivity.error != null) {
            return (
                <BlockContent>
                    <ErrorBlock>
                        <BlockContent>
                            <small className="blueeit-error">
                                Unable to load latest activity.
                            </small>
                        </BlockContent>
                    </ErrorBlock>
                </BlockContent>
            );
        }

        return (
            <ActivityNodeList activities={latestActivity.items} />
        )
    }

    return (
        <>
            <BodyHeader title={"What's New"} />
            <PageLayout sidebar={<><StatsPanel/></>}>
                <Block>
                    <BlockHeader>
                        Latest Posts
                    </BlockHeader>
                    {renderLatestPosts()}
                </Block>
                <BlockSpacer />
                <Block>
                    <BlockHeader>
                        Latest Profile Posts
                    </BlockHeader>
                    {renderLatestProfilePosts()}
                </Block>
                <BlockSpacer />
                <Block>
                    <BlockHeader>
                        Latest Activity
                    </BlockHeader>
                    {renderLatestActivity()}
                </Block>
            </PageLayout>
        </>
    );
}