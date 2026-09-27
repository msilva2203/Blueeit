import Block from "../components/Block";
import BlockHeader from "../components/BlockHeader";
import BlockItem from "../components/BlockItem";
import BlockSpacer from "../components/BlockSpacer";
import PageLayout from "../components/PageLayout";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";

export default function AboutPage() {
    return (
        <>
            <PageLayout sidebar={<><OnlineMembersPanel/></>}>
            <Block>
                <BlockHeader>
                    About Blueeit
                </BlockHeader>
                <BlockItem>
                    <>
                    <p> Blueeit is a community forum built for discussion, conversation, and sharing ideas. </p> <p> Whether you're here to talk about games, share something you've created, ask a question, or simply join the conversation, Blueeit is a place for the community to connect. </p>
                    </>
                </BlockItem>
            </Block>
            <BlockSpacer />
            <Block>
                <BlockHeader>
                    Our Community
                </BlockHeader>
                <BlockItem>
                    <>
                    <p> Blueeit is organized into forums, giving each community its own space for discussions and topics. Browse existing threads, join conversations, or start one of your own. </p>
                    </>
                </BlockItem>
            </Block>
            <BlockSpacer />
            <Block>
                <BlockHeader>
                    Built with
                </BlockHeader>
                <BlockItem>
                    <>
                    <p> Blueeit is a full-stack web application built with modern web technologies. </p> <ul> <li>ASP.NET Core Web API</li> <li>C#</li> <li>React</li> <li>PostgreSQL</li> </ul>
                    </>
                </BlockItem>
            </Block>
            <BlockSpacer />
            <Block>
                <BlockHeader>
                    About the project
                </BlockHeader>
                <BlockItem>
                    <>
                    <p> Blueeit is an independent project created to explore full-stack application development, web architecture, APIs, databases, and modern frontend development. </p>
                    </>
                </BlockItem>
            </Block>
            </PageLayout>
        </>
    );
}